from flask import Flask, jsonify, request
import json
from keras.layers import Dense
from keras.models import Sequential
from keras.models import load_model
from keras import backend as K
from keras import Input, Model
import tensorflow as tf
import numpy as np
import datetime
import glob
import os

app = Flask(__name__)

# Defining constants
file_extension = '.h5'
path_to_models = 'C:\\kart_NN\\'

# Model variables
'''
model = Sequential()

model.add(Dense(units=15, activation='sigmoid', input_dim=5))
model.add(Dense(units=2, activation='softsign'))

model.compile(loss='mean_squared_error', optimizer='adam', metrics=['accuracy'])
'''
inputs = Input(shape=(21,))

turnBranch = Dense(units=15, activation='relu', input_dim=inputs.shape[0])(inputs)
turnBranch = Dense(units=1, activation='softsign', name="turn_output")(turnBranch)

accelerationBranch = Dense(units=15, activation='relu', input_dim=inputs.shape[0])(inputs)
accelerationBranch = Dense(units=1, activation='softsign', name="acceleration_output")(accelerationBranch)

model = Model(inputs=inputs, outputs=[turnBranch, accelerationBranch])

losses = {
    "turn_output": "mean_squared_error",
    "acceleration_output": "mean_squared_error",
}

model.compile(loss=losses, optimizer='adam', metrics=['accuracy'])


graph = tf.get_default_graph()

'''
127.0.0.1:5000/api/save?name=test - Ici le modèle aura un nom spécifique
127.0.0.1:5000/api/save - Ici le modèle aura le nom model_weights
'''


@app.route('/api/save', methods=['GET'])
def saveCurrentModel():
    now = datetime.datetime.now()
    name = None
    if 'name' in request.args:
        name = request.args.get('name')
    else:
        name = 'model_weights'
    model.save(path_to_models + name + '_' + now.strftime("%Hh%Mm_%d_%m_%Y-%S") + file_extension)
    return 'the model has been saved', 200


'''
La méthode envoie la liste de tous les fichiers listés dans la variable path_to_models
'''


@app.route('/api/list', methods=['GET'])
def listModelsFolder():
    list_of_models_complete_path = glob.glob(path_to_models + '*' + file_extension)
    list_of_models_only_filename = [os.path.basename(x).strip(file_extension) for x in list_of_models_complete_path]
    return jsonify(list_of_models_only_filename), 200


'''
127.0.0.1:5000/api/load?name=test_23h51m_01_03_2019-53
Dans cette méthode le paramètre name doit être envoyé (sans l'extension)
'''


@app.route('/api/load', methods=['GET'])
def loadModelFromName():
    if 'name' not in request.args:
        return 'name not found', 500
    global model
    ##del model -- > it generates an noNameError
    with graph.as_default():
        model = load_model(path_to_models + request.args.get('name') + file_extension)

    return 'the model has been loaded', 200


'''
Cette méthode ne reçoit pas de paramètre. Elle remet tout simplement le modèle à zero
'''


@app.route('/api/reset', methods=['GET'])
def createNewModel():
    session = K.get_session()
    for layer in model.layers:
        if hasattr(layer, 'kernel_initializer'):
            layer.kernel.initializer.run(session=session)
    return 'new model initialized', 200


'''
{
	"inputs": [[1,2,3,4,5],[1,2,3,4,5],[1,2,3,4,5],[1,2,3,4,5]], 
	"outputs": [1,1,1,1]
}
'''


@app.route('/api/train', methods=['PUT'])
def trainModel():
    if not request.json:
        return "Il faut etre en json", 400

    with graph.as_default():
        # Get back json inputs and outputs
        distances = np.array(request.json['inputs'])
        turn = np.array(request.json['turnOutputs'])
        acceleration = np.array(request.json['accelerationOutputs'])

        '''
        # Simple MLP inputs and outputs format
        inputs, outputs = simpleMlpInputsOutputsFormat(distances, turn, acceleration)
        model.fit(inputs, outputs, epochs=20, batch_size=100)

        '''
        # Get inputs and outputs format for MLP with two branches and two loss (one for acceleration and one for turn)
        model.fit(distances, {"turn_output": turn, "acceleration_output": acceleration}, epochs=200, batch_size=32)

    return 'finished training', 200


'''
[[1,2,3,4,5]]
'''


@app.route('/api/predict', methods=['PUT'])
def getPrediction():
    with graph.as_default():
        json_inputs = request.json['inputs']
        inputs = np.zeros((1, len(json_inputs)))
        for i in range(len(json_inputs)):
            inputs[0][i] = json_inputs[i]
        predictions = model.predict(inputs)
        print(predictions[0][0][0])

        # Response format for bi branch MLP
        json_response = jsonify(dict(turn=str(predictions[0][0][0]), acceleration=str(predictions[1][0][0])))

        # Response format for simple MLP
        # json_response = jsonify(dict(turn=str(predictions[0][0]), acceleration=str(predictions[0][1])))
    return json_response


def simpleMlpInputsOutputsFormat(distances, turn, acceleration):
    # Make an array of shape (output.size, 2) with the turn angle and the acceleration angle
    outputs = np.zeros((turn.shape[0], 2))
    outputs[0:outputs.shape[0], 0] = turn
    outputs[0:outputs.shape[0], 1] = acceleration
    return distances, outputs

if __name__ == '__main__':
    print("run main")

    ## Config for TF-GPU
    #config = tf.ConfigProto(
    #    gpu_options=tf.GPUOptions(per_process_gpu_memory_fraction=0.8)
    #    # device_count = {'GPU': 1}
    #)
    #config.gpu_options.allow_growth = True
    #session = tf.Session(config=config)
    ##

    app.run(host='0.0.0.0', debug=True)
