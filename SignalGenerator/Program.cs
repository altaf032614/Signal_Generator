// designing a signal generator using OOP
using System;

// namespace declaration
namespace SignalGenerator
{
    // main class
    class Program
    {
        // main method
        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Usage: SignalGenerator <signal> <amplitude> <frequency> <duration>");
                return;
            }
            string name = args[0].ToLower();
            double amplitude = Convert.ToDouble(args[1]);
            double frequency = Convert.ToDouble(args[2]);
            double duration = Convert.ToDouble(args[3]);
            double samplerate = Convert.ToDouble(args[4]);

            //clear the file
            File.WriteAllText("time.txt", string.Empty);
            File.WriteAllText("output.txt", string.Empty);
            
            // generating signal
            SignalGenerator signal = SignalDictionary.SetupSignal(name, amplitude, frequency, duration, samplerate);
            signal.GenerateSignal();
        }
    }   

    // Dictionary class
    static class SignalDictionary
    {
        private static readonly Dictionary<string, Func<string, double, double, double, double, SignalGenerator>> signalGenerators 
        = new Dictionary<string, Func<string, double, double, double, double, SignalGenerator>>(StringComparer.OrdinalIgnoreCase)
        {
            { "sine", (name, amplitude, frequency, duration, samplerate) => new SineSignal(name, amplitude, frequency, duration, samplerate) },
            { "square", (name, amplitude, frequency, duration, samplerate) => new SquareSignal(name, amplitude, frequency, duration, samplerate) },
            { "triangle", (name, amplitude, frequency, duration, samplerate) => new TriangleSignal(name, amplitude, frequency, duration, samplerate) },
            { "sawtooth", (name, amplitude, frequency, duration, samplerate) => new SawtoothSignal(name, amplitude, frequency, duration, samplerate) }
        };
    
        public static SignalGenerator SetupSignal(string name, double amplitude, double frequency, double duration, double samplerate)
        {
            if (signalGenerators.TryGetValue(name, out var constructor))
            {
                return constructor(name, amplitude, frequency, duration, samplerate);
            }
            else
            {
                throw new KeyNotFoundException("Invalid signal type: " + name);
            }
        }
    }

    // signal generator base class
    public abstract class SignalGenerator
    {
        public string Name { get; set; } = "Signal";
        public double Amplitude { get; set; }
        public double Frequency { get; set; }
        public double Duration { get; set; }
        public double SampleRate { get; set; } = 44100;

        // constructor
        protected SignalGenerator(string name, double amplitude, double frequency, double duration, double samplerate)
        {
            Name = name;
            Amplitude = amplitude;
            Frequency = frequency;
            Duration = duration;
            SampleRate = samplerate;
        }
        
        //defining the signal generator
        public abstract void GenerateSignal();
    }

    // sine signal child class
    public class SineSignal : SignalGenerator
    {
        // constructor
        public SineSignal(string name, double amplitude, double frequency, double duration, double samplerate) 
        : base(name, amplitude, frequency, duration, samplerate) { }
        public override void GenerateSignal()
        {
            Console.WriteLine("Generating Sine Signal...");
            int totalSamples = (int)(Duration * SampleRate);
            for (int i = 0; i < totalSamples; i++)
            {
                double time = i / SampleRate;
                double value = Amplitude * Math.Sin(2 * Math.PI * Frequency * time);
                File.AppendAllText("time.txt", time.ToString() + "\n");
                File.AppendAllText("output.txt", value.ToString() + "\n");

            }
            Console.WriteLine("Done!");
        }
    }

    // square signal child class
    public class SquareSignal : SignalGenerator
    {
        // constructor
        public SquareSignal(string name, double amplitude, double frequency, double duration, double samplerate) 
        : base(name, amplitude, frequency, duration, samplerate) { }
        public override void GenerateSignal()
        {
            Console.WriteLine("Generating Square Signal...");
            int totalSamples = (int)(Duration * SampleRate);
            for (int i = 0; i < totalSamples; i++)
            {
                double time = i / SampleRate;
                double value = Amplitude * Math.Sign(Math.Sin(2 * Math.PI * Frequency * time));
                File.AppendAllText("time.txt", time.ToString() + "\n");
                File.AppendAllText("output.txt", value.ToString() + "\n");            
                }
            Console.WriteLine("Done!");
        }
    }

    // triangle signal child class
    public class TriangleSignal : SignalGenerator
    {
        // constructor
        public TriangleSignal(string name, double amplitude, double frequency, double duration, double samplerate) 
        : base(name, amplitude, frequency, duration, samplerate) { }
        public override void GenerateSignal()
        {
            Console.WriteLine("Generating Triangle Signal...");
            int totalSamples = (int)(Duration * SampleRate);
            for (int i = 0; i < totalSamples; i++)
            {
                double time = i / SampleRate;
                double value = Amplitude * Math.Asin(Math.Sin(2 * Math.PI * Frequency * time));
                File.AppendAllText("time.txt", time.ToString() + "\n");
                File.AppendAllText("output.txt", value.ToString() + "\n"); 
            }
            Console.WriteLine("Done!");
        }
    }

    // sawtooth signal child class
    public class SawtoothSignal : SignalGenerator
    {
        // constructor
        public SawtoothSignal(string name, double amplitude, double frequency, double duration, double samplerate) 
        : base(name, amplitude, frequency, duration, samplerate) { }
        public override void GenerateSignal()
        {
            Console.WriteLine("Generating Sawtooth Signal...");
            int totalSamples = (int)(Duration * SampleRate);
            for (int i = 0; i < totalSamples; i++)
            {
                double time = i / SampleRate;
                double value = Amplitude * (2 * (Frequency * time - Math.Floor(Frequency * time + 0.5)));
                File.AppendAllText("time.txt", time.ToString() + "\n");
                File.AppendAllText("output.txt", value.ToString() + "\n"); 
            }
            Console.WriteLine("Done!");
        }
    }
}

    