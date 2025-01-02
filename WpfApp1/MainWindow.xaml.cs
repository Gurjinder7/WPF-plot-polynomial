using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathNet.Numerics;
//using MathNet.Numerics.Integration;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Polynomial<double> poly = new Polynomial<double>(new double[] { 1, 2, 3 });

            //Polynomial myPolynomial = new Polynomial { Degree = 3, Coefficients = new decimal[] { 1, 2, 3 } };
            //BindingOperations.SetBinding(CoefficientText, TextBlock.TextProperty, new Binding("Coefficients") { Converter = new PolynomialConverter() });

            //double result = poly.Evaluate(2);

            // y = x^2
            // input will be in range of two values: 10-20, 2nd input = will be step size 
            // line y = ax + b, a = 2, b = 1



            //double[] dataX = { 1, 2, 3, 4, 5 };
            //double[] dataY = { 1, 4, 9, 16, 25 };

            //double[] dataX1 = {  };
            //double[] dataY1 = {  };

            //dataX1 = new double[5];
            //dataY1 = new double[5];

            //List<double> list = new List<double> {  };
            //List<double> list1 = new List<double> { };

            ////list.Add(4);

            //for (int i = 10; i < 21; i+=2)
            //{
            //    //dataX1[i-1] = i;
            //    //dataY1[i-1] = (2*i+1);

            //    list.Add(i);
            //    list1.Add(2 * i + 1);
            //}

            //double[] array1 = list.ToArray();
            //double[] array2 = list1.ToArray();


            //Debug.WriteLine(dataY1[2]);
            //Debug.WriteLine("DFSDFSD sdfsdfsdfsdf");


            //WpfPlot1.Plot.Add.Scatter(array1, array2);
            //WpfPlot1.Refresh();
        }

     

        private double Evaluate(double x, IEnumerable<double> coefficients)
            => coefficients.Select((a, i) => a * Math.Pow(x, i)).Sum();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string jj = @"([-]?[x*^\d]+)";
            Regex regex = new Regex(jj);

            //string input = "2x+3x^2-4x^1"; // example input string
            string inp = PolyInput.Text;

            if (inp.Trim().Length == 0) {
                MessageBox.Show("Empty polynomial text");
                return;
            }
            MatchCollection matches = regex.Matches(inp);

            List<double> coef = new List<double> { };
            List<double> pow = new List<double> { };

            int constantNum = 0;


            foreach (Match match in matches)
            {
                //Debug.WriteLine(match);
                string term = match.Value; // extracted polynomial term (e.g., "2x", "3x^2", "-4x^-1")
                                           // Process the term as needed (e.g., parse coefficient and exponent, store in a data structure)
                //Debug.WriteLine(term);
                //Regex reg2 = new Regex(@"([-]?\d)");
                Regex reg2 = new Regex(@"([-]?\dx\^\d|[-]?x\^\d|[-]?\d?x|[-]?\d)");

                //Debug.WriteLine(reg2.Match(term));
                MatchCollection matches1 = reg2.Matches(term);
                //Debug.WriteLine("----exp-----");

                /// [-] ?\dx\^\d | [-] ? x\^\d |\d? x|\d / gm
                //int coef1 = 1;
                //int pow1 = 1;
                //int count = 0;


                foreach (Match match1 in matches1) {
                    string term1 = match1.Value;
                    Debug.WriteLine(term1);

                    Regex reg3 = new Regex(@"(^[-]?\dx\^\d$)");  //-2x^2
                    Regex reg4 = new Regex(@"(^[-]?x\^\d$)");  //x^3
                    Regex reg5 = new Regex(@"(^[-]?\d?x$)"); //x or 2x
                    Regex reg6 = new Regex(@"(^[-]?\d$)"); // constant

                    //Debug.Write("---match----");
                    //Debug.WriteLine(reg3.Match(term));
                    Match match3 = reg3.Match(term1);
                    //Debug.WriteLine(match3.Success);

                    Match match4 = reg4.Match(term1);
                    //Debug.WriteLine(match4.Success);

                    Match match5 = reg5.Match(term1);
                    //Debug.WriteLine(match4.Success);

                    Match match6 = reg6.Match(term1);
                    //Debug.WriteLine(match4.Success);

                    Regex reg7 = new Regex(@"([-]?\d)");

                    MatchCollection matches3 = reg7.Matches(term1);

                    if (match3.Success)
                    {
                        Debug.Write("---match---3-");
                        Debug.WriteLine(term1);

                        //int count3 = 0;

                        if (term1[0] == '-')
                        {
                            coef.Add(-double.Parse(term1[1].ToString()));
                            pow.Add(double.Parse(term1[4].ToString()));

                        } else
                        {
                            coef.Add(double.Parse(term1[0].ToString()));
                            pow.Add(double.Parse(term1[3].ToString()));

                        }
                      

                    }

                    if (match4.Success)
                    {
                        Debug.Write("---match---4-");
                        Debug.WriteLine(term1);
                        
                        if (term1[0] == '-')
                        {
                            coef.Add(-1);
                            pow.Add(double.Parse(term1[3].ToString()));

                        }
                        else
                        {
                            coef.Add(1.0);
                            pow.Add(double.Parse(term1[2].ToString()));

                        }
                    }

                    if (match5.Success)
                    {
                        Debug.Write("---match---5-");
                        Debug.WriteLine(term1);

                        // here it works for 2x, but does not work for x only, needs condition for - as well

                        if (term1[0] == '-')
                        {
                            if (term1[1] == 'x')
                            {
                                coef.Add(-1);
                            }
                            else
                            {
                                coef.Add(-double.Parse(term1[1].ToString()));

                            }
                        }

                        if (term1[0] == 'x')
                        {
                            coef.Add(1);

                        }

                        if (Char.IsDigit(term1[0]))
                        {
                            coef.Add(double.Parse(term1[0].ToString()));
                        }

                        pow.Add(1);
                    }

                    if (match6.Success)
                    {
                        Debug.Write("---match---6-: ");
                        Debug.WriteLine(term1);

                        // need to consider - as well here
                        if (term1[0] == '-')
                        {
                            constantNum = int.Parse(term1);
                            Debug.Write("---- MINUS IS DETECTED FELLLAAAA---: ");
                            Debug.WriteLine(term1);
                            //constantNum = -constantNum;

                        } else
                        {
                            constantNum = int.Parse(term1);
                        }

                    }

              
                  
                }

            }

            double[] array1 = coef.ToArray();
            double[] array2 = pow.ToArray();

            foreach (int i in array1) {
                Debug.Write("---coef---");
                Debug.WriteLine(i);
            }

            foreach (int i in array2)
            {
                Debug.Write("----pow----");
                Debug.WriteLine(i);
            }

            List<double> list = new List<double> { };
            List<double> list1 = new List<double> { };


            Debug.WriteLine(array1.Length);
            Debug.WriteLine(array2.Length);
            Debug.Write("---powerrrrr------  ");
            //Debug.WriteLine(Math.Pow(3,1));

            string[] rangeText = RangeInput.Text.Split(',');
            double initialValue = double.Parse(rangeText[0]);
            double finalValue = double.Parse(rangeText[1]);

            double stepInput = double.Parse(StepInput.Text.ToString());

            for (double i = initialValue; i <= finalValue; i = i+ stepInput)
            {

                double sum = 0;
                //double valY = 0;
                for (int j = 0; j < array1.Length; j++)
                {
                    double pow1;
                    int index = Array.IndexOf(array2, j);
                    if (index == -1) {
                        pow1 = 1;
                    } else
                    {
                        pow1 = array2[j];
                    }

                    Debug.Write("----Math----");
                    Debug.WriteLine(Math.Pow(i, pow1));
                    sum = sum + (array1[j] * Math.Pow(i, array2[j]));

                Debug.Write("--sum---");
                    //Debug.WriteLine(i);
                    //Debug.WriteLine(j);
                    Debug.WriteLine(sum);
                }
                list.Add(i);
                list1.Add(sum + constantNum);

                Debug.Write("---const--: ");
                Debug.WriteLine(constantNum);

            }

            double[] array11 = list.ToArray();
            double[] array22 = list1.ToArray();


            foreach (int i in array11)
            {
                Debug.Write("---x---");
                Debug.WriteLine(i);
            }

            foreach (int i in array22)
            {
                Debug.Write("---y----");
                Debug.WriteLine(i);
            }


        


            WpfPlot1.Plot.Add.Scatter(array11, array22);
            WpfPlot1.Refresh();
            //WpfPlot1.Focus();
            //WpfPlot1.UpdateLayout();
        }

       
    }

}