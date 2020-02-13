using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IA.FuzzyLogic.Engine;
using IA.FuzzyLogic.Repositories;

namespace FuzzyLogicTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Point2D pt1 = new Point2D(0, 0);
            //Point2D pt2 = new Point2D(15, 0);
            //Point2D pt3 = new Point2D(17, 1);
            //Point2D pt4 = new Point2D(20, 1);
            //Point2D pt5 = new Point2D(25, 0);
            //Point2D pt6 = new Point2D(15, 0);


            //Console.WriteLine("Test Classe Point2D :");
            //Console.WriteLine("(15;0) == (15;0)" + (pt2 == pt6));
            //Console.WriteLine("(15;0) != (17;0)" + (pt2 != pt3));
            //Console.WriteLine("(15;0) > (17;1)" + (pt2 > pt3));
            //Console.WriteLine("(15;0) < (17;1)" + (pt2 < pt3));
            //Console.WriteLine("(15;0) > (15;0)" + (pt2 > pt6));
            //Console.WriteLine("(15;0) < (15;0)" + (pt2 < pt6));
            //Console.WriteLine("(15;0) >= (15;0)" + (pt2 >= pt6));
            //Console.WriteLine("(15;0) <= (15;1)" + (pt2 <= pt6));

            //Console.WriteLine("\nTest Classe FuzzySet :");
            //Point2D[] array = { pt5, pt2, pt6, pt4, pt1, pt3 };
            //FuzzySet fs = new FuzzySet(array);
            //Console.WriteLine("\n" + fs.ToString());

            //List<Point2D> list = new List<Point2D>();
            //list.Add(pt5);
            //list.Add(pt2);
            //list.Add(pt6);
            //list.Add(pt4);
            //list.Add(pt1);
            //list.Add(pt3);

            //FuzzySet fs2 = new FuzzySet(list);
            //fs2.Max = 53;
            //Console.WriteLine("\n" + fs2.ToString());
            //fs2.AddPoint(-9, 0.98);
            //fs2.Min = -8;
            //Console.WriteLine("\n" + fs2.ToString());
            //Console.WriteLine("Egalité des 2 ensembles flous ? " + (fs == fs2));
            //Console.WriteLine("Différence entre les 2 ensembles flous ? " + (fs != fs2));

            //FuzzySet fs3 = fs * 0.5;
            //FuzzySet fs4 = 0.5 * fs;
            //Console.WriteLine("Multiplication par 0.5 de fs : " + fs3.ToString());
            //Console.WriteLine("Multiplication par 0.5 de fs (autre sens) : " + fs4.ToString());
            //FuzzySet fs5 = !fs;
            //Console.WriteLine("Opération NON flou sur fs : " + fs5.ToString());
            //Console.WriteLine("Valeur d'appartenance de fs à x=15,5 (0,25 normalement) : " + fs.DegreeAtValue(15.5));
            //Console.WriteLine("Calcul du barycentre pour fs : " + fs.Barycenter);

            //RampFuzzySet rfs = new RampFuzzySet(11.54, 26.56);
            //Console.WriteLine("Test classe RampFuzzySet : \n" + rfs.ToString());
            //Console.WriteLine("Calcul du barycentre pour lfs : " + rfs.Barycenter);

            //TriangularFuzzySet tfs = new TriangularFuzzySet(52.25, 12.56, 25.89);
            //Console.WriteLine("Test classe TriangularFuzzySet : \n" + tfs.ToString());
            //Console.WriteLine("Calcul du barycentre pour tfs : " + tfs.Barycenter);

            //TrapezoidalFuzzySet trfs = new TrapezoidalFuzzySet(25.2, 5, 89.45, 39.45);
            //Console.WriteLine("Test classe TrapezoidalFuzzySet : \n" + trfs.ToString());
            //Console.WriteLine("Calcul du barycentre pour trfs : " + trfs.Barycenter);

            //Console.WriteLine("\nTest classe LinguisticVariable :");
            //LinguisticValue lv1 = new LinguisticValue("LV1", rfs);
            //LinguisticValue lv2 = new LinguisticValue("LV2", tfs);
            //LinguisticValue lv3 = new LinguisticValue("LV3", trfs);
            //List<LinguisticValue> listeLv1 = new List<LinguisticValue>();
            //listeLv1.Add(lv1);
            //listeLv1.Add(lv2);
            //listeLv1.Add(lv3);
            //LinguisticVariable lVar1 = new LinguisticVariable("Variable linguistique 1", listeLv1, 3.56, 141.25);
            //Console.WriteLine("MinValue = " + lVar1.MinValue + ", MaxValue = " + lVar1.MaxValue);


            // Test algorithme flou avec problème de gestion du zoom GPS
            // Définition variable linguistique 1 (Premisse)
            LinguisticVariable distance = new LinguisticVariable("Distance");
            distance.AddValue(new LinguisticValue("faible", new RampFuzzySet(30, 50, 0, 500000)));
            distance.AddValue(new LinguisticValue("moyenne", new TrapezoidalFuzzySet(40, 50, 100, 150, 0, 500000)));
            distance.AddValue(new LinguisticValue("grande", new RampFuzzySet(150, 100, 0, 500000)));
            Console.WriteLine("Contrôle de la variable linguistique distance : " + distance.MinValue + " " + distance.MaxValue);
            // Définition variable linguistique 2 (Premisse)
            LinguisticVariable vitesse = new LinguisticVariable("Vitesse");
            vitesse.AddValue(new LinguisticValue("lente", new RampFuzzySet(20, 30, 0, 200)));
            vitesse.AddValue(new LinguisticValue("peu rapide", new TrapezoidalFuzzySet(20, 30, 70, 80, 0, 200)));
            vitesse.AddValue(new LinguisticValue("rapide", new TrapezoidalFuzzySet(70, 80, 90, 110, 0, 200)));
            vitesse.AddValue(new LinguisticValue("très rapide", new RampFuzzySet(110, 90, 0, 200)));
            Console.WriteLine("Contrôle de la variable linguistique vitesse : " + vitesse.MinValue + " " + vitesse.MaxValue);
            // Définition variable linguistique 3 (Conclusion)
            LinguisticVariable zoom = new LinguisticVariable("Zoom");
            zoom.AddValue(new LinguisticValue("petit", new RampFuzzySet(1, 2, 0, 5)));
            zoom.AddValue(new LinguisticValue("normal", new TrapezoidalFuzzySet(1, 2, 3, 4, 0, 5)));
            zoom.AddValue(new LinguisticValue("gros", new RampFuzzySet(4, 3, 0, 5)));
            Console.WriteLine("Contrôle de la variable linguistique zoom : " + zoom.MinValue + " " + zoom.MaxValue);

            List<LinguisticVariable> listeLinguisticVariable = new List<LinguisticVariable>();
            listeLinguisticVariable.Add(distance);
            listeLinguisticVariable.Add(vitesse);

            // Définition des FuzzeExpression
            FuzzyExpression exp11 = new FuzzyExpression(distance, "faible");
            FuzzyExpression exp12 = new FuzzyExpression(distance, "moyenne");
            FuzzyExpression exp13 = new FuzzyExpression(distance, "grande");
            FuzzyExpression exp21 = new FuzzyExpression(vitesse, "lente");
            FuzzyExpression exp22 = new FuzzyExpression(vitesse, "peu rapide");
            FuzzyExpression exp23 = new FuzzyExpression(vitesse, "rapide");
            FuzzyExpression exp24 = new FuzzyExpression(vitesse, "très rapide");
            FuzzyExpression exp31 = new FuzzyExpression(zoom, "petit");
            FuzzyExpression exp32 = new FuzzyExpression(zoom, "normal");
            FuzzyExpression exp33 = new FuzzyExpression(zoom, "gros");
            // Définition des règles
            List<FuzzyExpression> premises = new List<FuzzyExpression>();
            FuzzyExpression conclusion;
            List<FuzzyRule> rules = new List<FuzzyRule>();
            // Règle 1
            premises.Add(exp11);
            premises.Add(exp21);
            conclusion = exp32;
            rules.Add(new FuzzyRule(new List<FuzzyExpression>(premises), conclusion));
            premises.Clear();

            // Règle 2
            premises.Add(exp12);
            premises.Add(exp21);
            conclusion = exp31;
            rules.Add(new FuzzyRule(new List<FuzzyExpression>(premises), conclusion));
            premises.Clear();

            // Règle 3
            premises.Add(exp11);
            premises.Add(exp22);
            conclusion = exp32;
            rules.Add(new FuzzyRule(new List<FuzzyExpression>(premises), conclusion));
            premises.Clear();

            // Règle 4
            premises.Add(exp12);
            premises.Add(exp22);
            conclusion = exp32;
            rules.Add(new FuzzyRule(new List<FuzzyExpression>(premises), conclusion));
            premises.Clear();

            // Règle 5
            premises.Add(exp11);
            premises.Add(exp23);
            conclusion = exp33;
            rules.Add(new FuzzyRule(new List<FuzzyExpression>(premises), conclusion));
            premises.Clear();

            // Règle 6
            premises.Add(exp12);
            premises.Add(exp23);
            conclusion = exp32;
            rules.Add(new FuzzyRule(new List<FuzzyExpression>(premises), conclusion));
            premises.Clear();

            // Règle 7
            premises.Add(exp11);
            premises.Add(exp24);
            conclusion = exp33;
            rules.Add(new FuzzyRule(new List<FuzzyExpression>(premises), conclusion));
            premises.Clear();

            // Règle 8
            premises.Add(exp12);
            premises.Add(exp24);
            conclusion = exp33;
            rules.Add(new FuzzyRule(new List<FuzzyExpression>(premises), conclusion));
            premises.Clear();

            // Règle 9
            premises.Add(exp13);
            conclusion = exp31;
            rules.Add(new FuzzyRule(new List<FuzzyExpression>(premises), conclusion));
            premises.Clear();

            // Création du FuzzySystem
            FuzzySystem system = new FuzzySystem("Gestion du zoom GPS", listeLinguisticVariable, zoom, rules);
            // Cas 1
            system.SetInputVariable(distance, 70);
            system.SetInputVariable(vitesse, 35);
            Console.WriteLine("GPS cas n°1 (70m et 35km/h) : " + system.Solve());
            system.ResetCase();

            // Cas 2
            system.SetInputVariable(distance, 70);
            system.SetInputVariable(vitesse, 25);
            Console.WriteLine("GPS cas n°1 (70m et 25km/h) : " + system.Solve());
            system.ResetCase();

            // Cas 3
            system.SetInputVariable(distance, 40);
            system.SetInputVariable(vitesse, 72.5);
            Console.WriteLine("GPS cas n°1 (40m et 72,5km/h) : " + system.Solve());
            system.ResetCase();

            // Cas 4
            system.SetInputVariable(distance, 110);
            system.SetInputVariable(vitesse, 100);
            Console.WriteLine("GPS cas n°1 (110m et 100km/h) : " + system.Solve());
            system.ResetCase();

            // Cas 5
            system.SetInputVariable(distance, 160);
            system.SetInputVariable(vitesse, 45);
            Console.WriteLine("GPS cas n°1 (160m et 45km/h) : " + system.Solve());
            system.ResetCase();


            // Test Parsin fichier XML
            Console.WriteLine("\n\nTEST PARSING FICHIER XML");
            XmlFuzzySystemParser parser = new XmlFuzzySystemParser();
            parser.CreateFuzzySystem("Fuzzy System - Gestion zoom GPS.xml");
            Console.WriteLine("Résultat parsing = " + parser.ErrorCode.ToString());
            List<LinguisticVariable> _inputs = parser.GetInputs();
            LinguisticVariable _output = parser.GetOutput();
            List<FuzzyRule> _rules = parser.GetRules();
            // Création du FuzzySystem
            system = new FuzzySystem("Gestion du zoom GPS", _inputs, _output, _rules);
            // Cas 1
            system.SetInputVariable(_inputs[0], 70);
            system.SetInputVariable(_inputs[1], 35);
            Console.WriteLine("GPS cas n°1 (70m et 35km/h) : " + system.Solve());
            system.ResetCase();

            // Cas 2
            system.SetInputVariable(_inputs[0], 70);
            system.SetInputVariable(_inputs[1], 25);
            Console.WriteLine("GPS cas n°1 (70m et 25km/h) : " + system.Solve());
            system.ResetCase();

            // Cas 3
            system.SetInputVariable(_inputs[0], 40);
            system.SetInputVariable(_inputs[1], 72.5);
            Console.WriteLine("GPS cas n°1 (40m et 72,5km/h) : " + system.Solve());
            system.ResetCase();

            // Cas 4
            system.SetInputVariable(_inputs[0], 110);
            system.SetInputVariable(_inputs[1], 100);
            Console.WriteLine("GPS cas n°1 (110m et 100km/h) : " + system.Solve());
            system.ResetCase();

            // Cas 5
            system.SetInputVariable(_inputs[0], 160);
            system.SetInputVariable(_inputs[1], 45);
            Console.WriteLine("GPS cas n°1 (160m et 45km/h) : " + system.Solve());
            system.ResetCase();



            Console.ReadKey();
        }
    }
}
