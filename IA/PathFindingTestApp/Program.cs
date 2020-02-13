using System;
using IA.PathFinding.Interfaces;
using IA.PathFinding.Engine;
using IA.PathFinding.ExempleMap;

namespace PathFindingTestApp
{
    class Program : IUserInterface
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();

        }

        public Program()
        {
        }

        private void Run()
        {
            // 1st map
            String mapStr = "..  XX   .\n"
                          + "*.  *X  *.\n"
                          + " .  XX ...\n"
                          + " .* X *.* \n"
                          + " ...=...  \n"
                          + " .* X     \n"
                          + " .  XXX*  \n"
                          + " .  * =   \n"
                          + " .... XX  \n"
                          + "   *.  X* ";
            Map map = new Map(mapStr, 0, 0, 9, 9);
            Algorithm algo = new DepthFirst(map, this);
            algo.Solve();
            Console.WriteLine();
            algo = new BreadthFirst(map, this);
            algo.Solve();
            Console.WriteLine();
            algo = new BellmanFord(map, this);
            algo.Solve();
            Console.WriteLine();
            algo = new Dijkstra(map, this);
            algo.Solve();
            Console.WriteLine();
            algo = new AStar(map, this);
            algo.Solve();
            Console.WriteLine();

            // 2nd map
            mapStr = "...*     X .*    *  \n"
                   + " *..*   *X .........\n"
                   + "   .     =   *.*  *.\n"
                   + "  *.   * XXXX .    .\n"
                   + "XXX=XX   X *XX=XXX*.\n"
                   + "  *.*X   =  X*.  X  \n"
                   + "   . X * X  X . *X* \n"
                   + "*  .*XX=XX *X . XXXX\n"
                   + " ....  .... X . X   \n"
                   + " . *....* . X*. = * ";
            map = new Map(mapStr, 0, 0, 9, 19);
            algo = new DepthFirst(map, this);
            algo.Solve();
            Console.WriteLine();
            algo = new BreadthFirst(map, this);
            algo.Solve();
            Console.WriteLine();
            algo = new BellmanFord(map, this);
            algo.Solve();
            Console.WriteLine();
            algo = new Dijkstra(map, this);
            algo.Solve();
            Console.WriteLine();
            algo = new AStar(map, this);
            algo.Solve();
            Console.WriteLine();

            Console.ReadKey();
        }

        public void PrintResult(string a_path, double a_distance)
        {
            Console.WriteLine("Path (size : " + a_distance + ") : " + a_path);
        }
    }
}