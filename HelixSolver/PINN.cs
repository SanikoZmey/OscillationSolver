using System.Collections.Generic;
using TorchSharp;
using static TorchSharp.torch;
using static TorchSharp.torch.nn;

namespace HelixSolver
{
    /// <summary>
    /// Class for a PINN model construction
    /// </summary>
    public class NeuralNetwork : nn.Module<Tensor, Tensor>
    {
        /// <summary>
        /// Module of all the model's layers (read only)
        /// </summary>
        private readonly Module<Tensor, Tensor> layers;
        
        /// <summary>
        /// Constructor of the PINN model
        /// </summary>
        /// <param name="numHiddenLayers">Number of hidden layers</param>
        /// <param name="hiddenSize">Number of neurons in each hidden layer</param>
        /// <param name="activationFunctionName">Name of activation function for neurons in hidden layers(if any)</param>
        public NeuralNetwork(int numHiddenLayers, int hiddenSize, string activationFunctionName) : base(nameof(NeuralNetwork))
        {
            List<(string, nn.Module<Tensor, Tensor>)> modules = new List<(string, nn.Module<Tensor, Tensor>)>();

            modules.Add(("input_layer", nn.Linear(1, hiddenSize, dtype: torch.float64)));
            addActivationFunction(modules, activationFunctionName);
            
            for (int i = 0; i < numHiddenLayers; i++)
            {
                modules.Add(($"hiden_layer{i + 1}", nn.Linear(hiddenSize, hiddenSize, dtype: torch.float64)));
                addActivationFunction(modules, activationFunctionName);
            }
            modules.Add(("output_layer", nn.Linear(hiddenSize, 1, dtype: torch.float64)));

            layers = nn.Sequential(modules);

            RegisterComponents();
        }

        /// <summary>
        /// Private method for adding a particular activation function to model layers
        /// </summary>
        /// <param name="modules">List of all the named layers of the model</param>
        /// <param name="functonName">Name of the activation funciton to be added</param>
        private void addActivationFunction(List<(string, nn.Module<Tensor, Tensor>)> modules, string functonName)
        {
            switch (functonName)
            {
                case "hardTanh":
                    modules.Add(("HardTanh", nn.Hardtanh()));
                    break;
                case "Tanhshrink":
                    modules.Add(("Tanhshrink", nn.Tanhshrink()));
                    break;
                case "Mish":
                    modules.Add(("Mish", nn.Mish()));
                    break;
                case "APTx":
                    modules.Add(("APTx(alpha = 1, beta = 1, gamma = 1/2)", new APTx("APTx(alpha = 1, beta = 1, gamma = 1/2)")));
                    break;
                default:
                    modules.Add(("Tanh", nn.Tanh()));
                    break;
            }
        }

        /// <summary>
        /// Method for performing a forward pass through the model
        /// </summary>
        /// <param name="input">Tensor of time-steps</param>
        /// <returns>Result of a forward pass</returns>
        public override Tensor forward(Tensor input)
        {
            return layers.forward(input);
        }
    }

    /// <summary>
    /// Class for a APTx activation function (https://arxiv.org/abs/2209.06119)
    /// </summary>
    public class APTx : nn.Module<Tensor, Tensor>
    {
        /// <summary>
        /// Constructor for a APTx activation function
        /// </summary>
        /// <param name="name">Name of the layer</param>
        public APTx(string name) : base(name) {}

        /// <summary>
        /// Method for performing a forward pass through the function
        /// </summary>
        /// <param name="x">Tensor of time-steps</param>
        /// <returns>Result of a forward pass</returns>
        public override Tensor forward(Tensor x)
        {
            return (1 + torch.tanh(x)) * x / 2;
        }
    }
}
