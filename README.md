This project was made in the context of open days at HEG Geneva for the IT branch. We use the Karting Microgame open source game given by Unity (https://learn.unity.com/project/karting-template) as a base and extend it to create our own IA.

As we wanted this activity to be attractive, we decided to let users play the game and get back their actions to feed a Neural Network in backend. This wasn't the solution who gave us the best results, however it was the one who allow us to attract students and then to be able to explain deeper how Machine Learning algorithm work.

The IA created and trained with users actions achieve to terminate at least one circuit and can obviously perform better with some Generative algorithm.

To test this project on your own, you need to clone this repository. Then you must have either the Neural Network and the game launched on the same PC or launch them on two differents machine but make sure they can communicate through HTTP. With two machines, you can simply connect them to the same network and just use *ping* command to be sure they can communicate.

On the machine you want to run the Neural Network, make sure to install [Python3](https://www.python.org/downloads/) and we recommand to use [Anaconda](https://www.anaconda.com/) as a package and environment manager. Then with Anaconda you can create an environnement based on the requirement.yml file in the HEG-Kart_IA/ directory by using the command : *conda env create -f environment.yml*.

When all dependencies are installed, you can run the main python Neural Network script by using this command in command prompt : *python rest_flask.py*.

Now the Neural Network is ready to receive data and train on it on your machine. You can now launch the game and just go to the "Settings" menu to change the ip address of the server where the Neural Network is running.

Once you played some circuits yourself and you saw the Neural Network training in the console of the server, you can either launch another instance of the game or just switch the "Player" mode to "IA" in your current game party.

You will now see the IA who's sending request to the trained Neural Network to know which action to do every frame. And...that's it ! Feel free to download the project and test your own model by changing the code in the *rest_flask.py* python file.