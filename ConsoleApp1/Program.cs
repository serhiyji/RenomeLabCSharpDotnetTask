using System;

namespace ConsoleApp1
{
    public abstract class SystemPPO
    {
        public string Name { get; protected set; }
        public int ProbabilityOfHit { get; protected set; }
        public int CountTargetsHit { get; protected set; } = 0;
        public int CountTargetsMissed { get; protected set;} = 0;
        public static int AllCountTargetsHit { get; protected set; } = 0;
        public static int AllCountTargetsMissed { get; protected set; } = 0;
        public event EventHandler TargetHit;
        public event EventHandler TargetMissed;

        public SystemPPO(string name, int probabilityOfHit)
        {
            this.Name = name;
            this.ProbabilityOfHit = probabilityOfHit;
            TargetHit += TargetHitHandler;
            TargetMissed += TargetMissedHandler;
        }

        protected virtual void TargetHitHandler(object? sender, EventArgs e)
        {
            AllCountTargetsHit++;
            this.CountTargetsHit++;
        }

        protected virtual void TargetMissedHandler(object? sender, EventArgs e)
        {
            AllCountTargetsMissed++;
            this.CountTargetsMissed++;
        }

        public static void PrintAllInfo()
        {
            Console.WriteLine("All System PPO");
            Console.WriteLine($"Total Targets : {AllCountTargetsHit + AllCountTargetsMissed}");
            Console.WriteLine($"Count Targets Hit : {AllCountTargetsHit}");
            Console.WriteLine($"Count Targets Missed : {AllCountTargetsMissed}");
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"System PPO {this.Name} : ");
            Console.WriteLine($"Total Targets : {this.CountTargetsHit + this.CountTargetsMissed}");
            Console.WriteLine($"Count Targets Hit : {this.CountTargetsHit}");
            Console.WriteLine($"Count Targets Missed : {this.CountTargetsMissed}");
        }

        public void Fire()
        {
            int randomNumber = new Random().Next(1, 101);
            ((randomNumber <= this.ProbabilityOfHit) ? TargetHit : TargetMissed)?.Invoke(this, EventArgs.Empty);
        }
    }

    public class C300 : SystemPPO
    {
        public C300(int probabilityOfHit) : base("C300", probabilityOfHit) { }
    }

    public class S400 : SystemPPO
    {
        public S400(int probabilityOfHit) : base("C400", probabilityOfHit) { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int numberTargets = 10;

            C300 c300 = new C300(60);
            S400 s400 = new S400(70);

            for (int i = 0; i < numberTargets; i++)
            {
                c300.Fire();
                s400.Fire();
            }

            SystemPPO.PrintAllInfo();
            Console.WriteLine("----------");
            c300.PrintInfo();
            Console.WriteLine("----------");
            s400.PrintInfo();
        }
    }
}
