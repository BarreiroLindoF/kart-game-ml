using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class TestingData
{
    [JsonProperty]
    public List<float> inputs { get; set; }


    public TestingData()
    {
        this.inputs = new List<float>();
    }

}
