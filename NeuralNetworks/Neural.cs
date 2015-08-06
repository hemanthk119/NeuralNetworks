using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NeuralNetworks
{
    public class Neuron
    {
        public readonly int Index;
        public double AccumulateStore { get; set; }
        private double val;
        public double Value { get { return val; } set { val = value; if (SelfLayer != null)SelfLayer.RaiseChanged(); } }
        public double Bias { get; set; }
        public ActivationType ActType { get; set; }
        public enum ActivationType { BINARY, BIPOLAR, SIGMOID, BIPOLARSIGMOID, RAMP };

        public List<Connection> Connections { get; set; }

        public Layer SelfLayer { get; set; }

        public Neuron(int index, Layer initLayer, Neuron.ActivationType actType = ActivationType.SIGMOID, double bias = 0)
        {
            Index = index;
            Connections = new List<Connection>();
            SelfLayer = initLayer;
            ActType = actType;
            Bias = bias;
        }

        public void AddConnection(Neuron neuron, bool to, double weight = 0/*, double bias = 0*/)
        {
            Connection c;

            if (to)
                c = new Connection(this, neuron, weight);
            else
                c = new Connection(neuron, this, weight);

            this.Connections.Add(c);
            neuron.Connections.Add(c);
        }

        public double CalculateValue()  //NEURON UPDATE
        {
            double val = 0;

            IEnumerable<Connection> inputConns = Connections.Where(r => r.toNeuron == this);

            foreach (Connection c in inputConns)
            {
                val += (c.fromNeuron.Value * c.Weight);
            }

            if (inputConns.Count() > 0)
            {
                val += Bias;
                AccumulateStore = val;
                Value = Activate(val);
            }

            //Console.WriteLine("NeCal: Acc:" + AccumulateStore + ", Val: " + Value + "Bia: " + Bias );

            return Value;
        }

        public double Activate(double input)
        {
            switch (ActType)
            {
                case ActivationType.BINARY:
                    if (input > 0)
                        return 1;
                    else
                        return 0;

                case ActivationType.BIPOLAR:

                    if (input > 0)
                        return 1;
                    else
                        return -1;

                case ActivationType.RAMP:
                    return input;

                case ActivationType.SIGMOID:
                    return 1 / (1 + Math.Pow(Math.E, -1 * input));

                default:
                    return 0;
                case ActivationType.BIPOLARSIGMOID:
                    return (2 / (1 + Math.Pow(Math.E, -1 * input))) - 1;

            }
        }

    }

    public class RandomProvider
    {
        public static Random random = new Random();
    }


    public class Connection
    {



        public readonly Neuron toNeuron;
        public readonly Neuron fromNeuron;
        public double Weight { get; set; }
        //public double Bias { get; set; }

        public Connection(Neuron from, Neuron to)
        {
            toNeuron = to;
            fromNeuron = from;
            Weight = RandomProvider.random.NextDouble() * 0.5;
        }

        public Connection(Neuron from, Neuron to, double weight)
        {
            toNeuron = to;
            fromNeuron = from;
            Weight = weight;
            //Bias = bias;
        }
    }

    public class Layer
    {
        //Type
        //NumNeurons
        //Hard index in NN
        //Nerons Array

        public event ChangedHandler Changed;
        public delegate void ChangedHandler(Layer l, EventArgs e);

        public event UpdatedHandler Updated;
        public delegate void UpdatedHandler(Layer l, EventArgs e);

        public readonly int Index;
        public readonly int NumNeurons;
        public Neuron.ActivationType ActType { get; set; }
        public Neuron[] Neurons;

        public void RaiseChanged()
        {
            if(Changed!= null)
                Changed(this, EventArgs.Empty);
        }
        private void RaiseUpdated()
        {
            if(Updated!=null)
                Updated(this, EventArgs.Empty);
        }

        public Layer(int index, int numNeurons, Neuron.ActivationType actTye = Neuron.ActivationType.SIGMOID, IEnumerable<double> initBias = null)
        {
            Index = index;
            NumNeurons = numNeurons;
            ActType = actTye;
            Neurons = new Neuron[NumNeurons];


            if (initBias != null)
            {
                for (int i = 0; i < NumNeurons; i++)
                {
                    Neurons[i] = new Neuron(i, this, ActType, initBias.ElementAt(i));
                }
            }

            else
            {
                for (int i = 0; i < NumNeurons; i++)
                {
                    Neurons[i] = new Neuron(i, this, ActType);
                }
            }
        }

        public void SetValues(IEnumerable<double> Values)
        {
            if (Values.Count() == NumNeurons)
            {
                for (int i = 0; i < Values.Count(); i++)
                {
                    Neurons.ElementAt(i).Value = Values.ElementAt(i);
                }
            }
        }

        public IEnumerable<double> GetValues()
        {
            IEnumerable<double> values = this.Neurons.Select(r => r.Value);
            return values;
        }

        public IEnumerable<double> CalculateLayer()
        {
            Parallel.ForEach(Neurons, new Action<Neuron>((n) =>
            {
                n.CalculateValue();                
            }));
            RaiseUpdated();
            return Neurons.Select(r => r.Value);
        }

        public void ConnectAllTo(Layer nextlayer, double defaultWeight = 0)
        {
            //Connects all neurons in current layer to the neurons in the next 

            foreach (Neuron n1 in Neurons)
            {
                foreach (Neuron n2 in nextlayer.Neurons)
                {
                    n1.AddConnection(n2, true, RandomProvider.random.NextDouble() - 0.5);
                }
            }

        }
    }

    public class NeuralNetwork
    {
        //Array of layers
        //Index of inputlayer
        //Index of outputlayer

        public Layer[] Layers { get; set; }
        public int InputIndex { get; set; }
        public int OutputIndex { get; set; }

        public readonly int NumLayers;

        public NeuralNetwork(int numLayers)
        {
            Layers = new Layer[numLayers];
            NumLayers = numLayers;
        }

        public NeuralNetwork(NetworkData network)
        {
            Layers = new Layer[network.Layers.Count];

            NumLayers = network.Layers.Count;
            foreach (LayerData ld in network.Layers)
            {

                Layers[network.Layers.IndexOf(ld)] = new Layer(network.Layers.IndexOf(ld), ld.NumNeuron, ld.ActType, ld.Bias);
            }

            foreach (ConnectionData cd in network.Connections)
            {
                Layers[cd.From.Layer].Neurons[cd.From.Node].AddConnection(Layers[cd.To.Layer].Neurons[cd.To.Node], true, cd.Weight);
            }

            InputIndex = network.InputLayerId;
            OutputIndex = network.OutputLayerId;

        }

        public NetworkData GetNetworkData()
        {
            List<ConnectionData> conns = new List<ConnectionData>();
            List<LayerData> lays = new List<LayerData>();



            foreach (Layer layer in Layers)
            {
                LayerData newlayerData = new LayerData() { NumNeuron = layer.NumNeurons, ActType = layer.ActType, Bias = new List<double>() };
                int layerid = layer.Index;
                foreach (Neuron neuron in layer.Neurons)
                {
                    int neuronid = neuron.Index;

                    newlayerData.Bias.Add(neuron.Bias);

                    foreach (Connection conn in neuron.Connections.Where(r => r.fromNeuron == neuron))
                    {
                        conns.Add(new ConnectionData()
                        {
                            From = new NeuronData() { Layer = layerid, Node = neuronid },
                            To = new NeuronData() { Layer = conn.toNeuron.SelfLayer.Index, Node = conn.toNeuron.Index },
                            Weight = conn.Weight

                        });
                    }
                }
                lays.Add(newlayerData);
            }

            return new NetworkData() { Connections = conns, InputLayerId = InputIndex, OutputLayerId = OutputIndex, Layers = lays };
        }

        public static void SaveNetworkToFile(NetworkData netdata, string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(NetworkData));
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, netdata);
                writer.Close();
            }

            catch
            {
                MessageBox.Show("File Not Saved: ", filename);
            }

        }

        public static NetworkData ReadNetworkFromFile(string filename)
        {
            NetworkData data;

            if (File.Exists(filename))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(NetworkData));
                    FileStream stream = new FileStream(filename, FileMode.Open);
                    data = (NetworkData)serializer.Deserialize(stream);
                    stream.Close();
                }

                catch
                {
                    MessageBox.Show("Network File Read Error: " + filename, "Error");
                    data = new NetworkData();
                }
            }

            else
            {
                MessageBox.Show("File Not Found: " + filename, "Not Found");
                data = new NetworkData();
            }

            return data;
        }

        public static IEnumerable<DataSet> ReadDataSetFromFile(string filename)
        {
            List<DataSet> dataSet = null;

            if (File.Exists(filename))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DataSet>));
                FileStream file = new FileStream(filename, FileMode.Open);

                try
                {
                    dataSet = (List<DataSet>)serializer.Deserialize(file);
                    file.Close();
                }

                catch
                {
                    MessageBox.Show("DataSet file '" + filename + "' read error", "Read error");
                    file.Close();
                }
            }

            else
            {
                MessageBox.Show("DataSet File does not exist\n" + filename, "Read DataSet");
            }

            return dataSet;
        }

        public static void WriteDataSetToFile(IEnumerable<DataSet> dataset, string filename)
        {
            if (dataset != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DataSet>));
                TextWriter textWriter = new StreamWriter(filename);
                serializer.Serialize(textWriter, dataset);
                textWriter.Close();
            }
        }

        public virtual void ApplyInput(IEnumerable<double> input)
        {
            Layers[InputIndex].SetValues(input);
        }

        public virtual IEnumerable<double> ReadOutput()
        {
            return Layers[OutputIndex].GetValues();
        }

        public virtual void CalculateOutput()//TEST
        {

            //ASSUMING FORWARD FEED
            for (int i = 0; i < NumLayers; i++)
            {
                Layers[i].CalculateLayer();
            }

        }
    }

    public class BackPropogationNetwork : NeuralNetwork
    {
        private List<List<List<double>>> PrevDW = null;
        private List<List<double>> deltaArr = null;

        public BackPropogationNetwork(NetworkData data)
            : base(data)
        {
            if (data != null)
                //MessageBox.Show("File Read");
                ;
            else
                MessageBox.Show("From dll: File Not Found");
        }

        public BackPropogationNetwork(int numInputs, int numOutputs, int numHidden, int numHiddenLayers = 1)
            : base(numHiddenLayers + 2)
        {
            Layers[0] = new Layer(0, numInputs, Neuron.ActivationType.RAMP);
            Layers[2 + numHiddenLayers - 1] = new Layer(2 + numHiddenLayers - 1, numOutputs, Neuron.ActivationType.SIGMOID);
            for (int i = 0; i < numHiddenLayers; i++)
            {
                Layers[i + 1] = new Layer(i + 1, numHidden, Neuron.ActivationType.SIGMOID);
            }

            for (int i = 0; i < NumLayers - 1; i++)
            {
                Layers[i].ConnectAllTo(Layers[i + 1]);
            }

            InputIndex = 0;
            OutputIndex = NumLayers - 1;
        }

        public List<List<List<double>>> BackPropogate(DataSet datain, double learninnRate, double momentum = 0)
        {

            if (PrevDW == null)
            {
                PrevDW = new List<List<List<double>>>();
                deltaArr = new List<List<double>>();
                for (int i = 0; i < NumLayers; i++)
                {
                    deltaArr.Add(new List<double>());
                    PrevDW.Add(new List<List<double>>());


                    for (int n = 0; n < Layers[i].NumNeurons; n++)
                    {
                        deltaArr[i].Add(0);
                        PrevDW[i].Add(new List<double>());

                        IEnumerable<Connection> outConn = Layers[i].Neurons[n].Connections.Where(r => r.toNeuron == Layers[i].Neurons[n]);


                        int d = 0;
                        foreach (Connection c in outConn)
                        {
                            PrevDW[i][n].Add(0);
                            d++;
                        }
                    }
                }
            }

            ApplyInput(datain.Inputs);
            CalculateOutput();

            Layer currentLayer = Layers[OutputIndex];
            while (currentLayer != Layers[InputIndex])
            {
                Parallel.ForEach(currentLayer.Neurons, new Action<Neuron>((n) =>
                {
                    double error = 0;

                    if (currentLayer == Layers[OutputIndex])
                    {
                        error = datain.Outputs[n.Index] - n.Value;
                    }

                    else
                    {
                        foreach (Connection c in n.Connections.Where(r => r.fromNeuron == n))
                        {
                            error += c.Weight * deltaArr[c.toNeuron.SelfLayer.Index][c.toNeuron.Index];
                        }

                    }

                    error = error * n.Value * (1 - n.Value);

                    deltaArr[currentLayer.Index][n.Index] = error;

                }));

                currentLayer = Layers[currentLayer.Index - 1];
            }

            currentLayer = Layers[OutputIndex];
            while (currentLayer != Layers[InputIndex])
            {



                for (int i = 0; i < currentLayer.NumNeurons; i++)
                {

                    Neuron n = currentLayer.Neurons[i];

                    foreach (Connection c in n.Connections.Where(r => r.toNeuron == n))
                    {
                        double dw = (deltaArr[c.toNeuron.SelfLayer.Index][c.toNeuron.Index] * learninnRate * c.fromNeuron.Value) + (momentum * PrevDW[currentLayer.Index][i][n.Connections.IndexOf(c)]);
                        c.Weight += dw;
                        PrevDW[currentLayer.Index][i][n.Connections.IndexOf(c)] = dw;

                    }

                    n.Bias += deltaArr[currentLayer.Index][i] * learninnRate;
                }



                currentLayer = Layers[currentLayer.Index - 1];
            }


            return PrevDW;
        }
        public void BatchBackPropogate(DataSet[] dataSet, int iterations, double leanrnignRate, double momentum = 0, BackgroundWorker worker = null)
        {
            int y =0;
            for (int i = 0; i < iterations; i++)
            {
                foreach (DataSet data in dataSet)
                {
                    BackPropogate(data, leanrnignRate, momentum);
                    y++;

                    if (worker != null)
                    {
                        worker.ReportProgress((int)(((double)y / (double)(iterations * dataSet.Count())) *100));
                    }
                }
            }
        }
    }
    
    //public class Adaline : NeuralNetwork
    //{
    //    public Adaline(int NumInputs)
    //        : base(2)
    //    {
    //        InputIndex = 0;
    //        OutputIndex = 1;

    //        Layers[InputIndex] = new Layer(InputIndex, NumInputs + 1, Neuron.ActivationType.RAMP);
    //        Layers[OutputIndex] = new Layer(OutputIndex, 1, Neuron.ActivationType.RAMP);

    //        Random r = new Random();
    //        foreach (Neuron n in Layers[InputIndex].Neurons)
    //        {
    //            n.AddConnection(Layers[OutputIndex].Neurons.First(), true, r.NextDouble() * 0.5);
    //        }
    //    }

    //    public Adaline(NetworkData data)
    //        : base(data)
    //    {

    //    }

    //    public override void ApplyInput(IEnumerable<double> input)
    //    {
    //        base.ApplyInput(input.Concat(new List<double>() { 1 }));
    //    }

    //    public void ApplyWeights(IEnumerable<double> Weights)
    //    {
    //        if (Weights.Count() == Layers[InputIndex].NumNeurons)
    //        {
    //            for (int i = 0; i < Weights.Count(); i++)
    //            {
    //                Layers[OutputIndex].Neurons.First().Connections[i].Weight = Weights.ElementAt(i);
    //            }
    //        }

    //    }

    //    public double GetOutput()
    //    {
    //        Layers[OutputIndex].CalculateLayer();
    //        return Layers[OutputIndex].GetValues().First();
    //    }

    //    public double GetBipolarOutput()
    //    {
    //        Layers[OutputIndex].CalculateLayer();
    //        if (Layers[OutputIndex].GetValues().First() > 0)
    //            return 1;
    //        else
    //            return -1;
    //    }

    //    public IEnumerable<double> GetWeights()
    //    {
    //        List<double> wt = new List<double>();   //
    //        for (int i = 0; i < Layers[OutputIndex].Neurons.First().Connections.Count; i++)
    //        {
    //            wt.Add(Layers[OutputIndex].Neurons.First().Connections[i].Weight);
    //        }

    //        return wt;
    //    }

    //    public void LearnFromData(DataSet Data, double learningRate)
    //    {
    //        ApplyInput(Data.Inputs);
    //        double error = Data.Outputs.First() - GetOutput();

    //        IEnumerable<double> oldWeights = GetWeights();
    //        List<double> newWeights = new List<double>();
    //        newWeights.AddRange(oldWeights);

    //        for (int i = 0; i < oldWeights.Count(); i++)
    //        {
    //            newWeights[i] = oldWeights.ElementAt(i) + (learningRate * error * Data.Inputs.Concat(new double[] { 1 }).ElementAt(i));
    //        }

    //        ApplyWeights(newWeights);
    //    }

    //    public void LearnFromDataSet(IEnumerable<DataSet> data, double learningRate)
    //    {
    //        foreach (DataSet dataele in data)
    //        {
    //            LearnFromData(dataele, learningRate);
    //        }
    //    }

    //}

    public class DataSet
    {
        public double[] Inputs { get; set; }
        public double[] Outputs { get; set; }
    }
    
    public class NetworkData
    {
        public List<LayerData> Layers { get; set; }
        public List<ConnectionData> Connections { get; set; }
        public int InputLayerId { get; set; }
        public int OutputLayerId { get; set; }

        public NetworkData()
        {
            Layers = new List<LayerData>();
            Connections = new List<ConnectionData>();
        }

    }
    
    public class ConnectionData
    {
        public NeuronData From { get; set; }
        public NeuronData To { get; set; }
        public double Weight { get; set; }
    }
    
    public class NeuronData
    {
        public int Layer { get; set; }
        public int Node { get; set; }
    }
    
    public class LayerData
    {
        public Neuron.ActivationType ActType { get; set; }
        public int NumNeuron { get; set; }
        public List<double> Bias { get; set; }
    }
}
