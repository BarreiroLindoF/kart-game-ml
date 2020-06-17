This project was made in the context of open days at HEG Geneva for the IT branch. We use the Karting Microgame open source game given by Unity (https://learn.unity.com/project/karting-template) as a base and extend it to create our own IA.

As we wanted this activity to be attractive, we decided to let users play the game and get back their actions to feed a Neural Network in backend. This wasn't the solution who gave us the best results, however it was the one who allow us to attract students and then to be able to explain deeper how Machine Learning algorithm work.

The IA created and trained with users actions achieve to terminate at least one circuit and can obviously perform better with some Generative algorithm.

To test this project on your own, you need to clone this repository. Then you must have either the Neural Network and the game launched on the same PC or launch them on two differents machine but make sure they can communicate through HTTP. With two machines, you can simply connect them to the same network and just use *ping* command to be sure they can communicate.