using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class TrainingData
{
    [JsonProperty]
    public List<List<float>> inputs { get; set; }
    [JsonProperty]
    public List<float> turnOutputs { get; set; }
    [JsonProperty]
    public List<float> accelerationOutputs { get; set; }


    public TrainingData() {
        this.inputs = new List<List<float>>();
        this.turnOutputs = new List<float>();
        this.accelerationOutputs = new List<float>();
    }

}
