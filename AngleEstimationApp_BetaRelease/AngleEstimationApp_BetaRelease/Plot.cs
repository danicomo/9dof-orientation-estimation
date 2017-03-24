using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace AngleEstimationApp_BetaRelease
{
    public partial class Plot : Form
    {
        private PointPairList listQ1;
        private PointPairList listQ2;
        private PointPairList listQ3;
        private PointPairList listQ4;

        private int index;
        public Plot()
        {
            InitializeComponent();
            index = 0;
            listQ1= new PointPairList();
            listQ2 = new PointPairList();
            listQ3 = new PointPairList();
            listQ4 = new PointPairList();
            this.Location = new Point(400, 100);
            Plot_Load(null, null);
        }

        private void Plot_Load(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1,zedGraphControl2,zedGraphControl3,zedGraphControl4);
        }

        private void CreateGraph(ZedGraphControl zgc1,ZedGraphControl zgc2,ZedGraphControl zgc3,ZedGraphControl zgc4)
        {
            // get a reference to the GraphPane

            GraphPane myPane1 = zgc1.GraphPane;
            GraphPane myPane2 = zgc2.GraphPane;
            GraphPane myPane3 = zgc3.GraphPane;
            GraphPane myPane4 = zgc4.GraphPane;

            // Set the Titles
            myPane1.Title.Text = "Real - Quaternion";
            myPane1.XAxis.Title.Text = "Step";
            myPane1.YAxis.Title.Text = "Value";

            LineItem myCurve = myPane1.AddCurve("Quaternion 0",
                  listQ1, Color.Red, SymbolType.Diamond);

            zgc1.AxisChange();

            // Set the Titles
            myPane2.Title.Text = "Quaternion i";
            myPane2.XAxis.Title.Text = "Step";
            myPane2.YAxis.Title.Text = "Value";

            LineItem myCurve2 = myPane2.AddCurve("Quaternion 1",
                  listQ2, Color.Red, SymbolType.Diamond);

            zgc2.AxisChange();

            // Set the Titles
            myPane3.Title.Text = "Quaternion j";
            myPane3.XAxis.Title.Text = "Step";
            myPane3.YAxis.Title.Text = "Value";

            LineItem myCurve3 = myPane3.AddCurve("Quaternion 2",
                  listQ3, Color.Red, SymbolType.Diamond);

            zgc3.AxisChange();

            // Set the Titles
            myPane4.Title.Text = "Quaternion k";
            myPane4.XAxis.Title.Text = "Step";
            myPane4.YAxis.Title.Text = "Value";

            LineItem myCurve4 = myPane4.AddCurve("Quaternion 3",
                  listQ4, Color.Red, SymbolType.Diamond);

            zgc4.AxisChange();
        }

        public void AddDataToGraph(float q1,float q2,float q3,float q4)
        {

            index++;
            bool flag = false;
            if (index > 500)
                flag = true;

            //First Quaternion Redraw

            // Get the first CurveItem in the graph
            LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;

            // Get the PointPairList
            IPointListEdit list = curve.Points as IPointListEdit;
            if (flag)
                list.RemoveAt(0);
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list == null)
                return;
            

            // add new data points to the graph
            list.Add(index, q1);

            // force redraw
            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();

            //Second Quaternion Redraw

            // Get the first CurveItem in the graph
            LineItem curve2 = zedGraphControl2.GraphPane.CurveList[0] as LineItem;

            // Get the PointPairList
            IPointListEdit list2 = curve2.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list2 == null)
                return;
            // add new data points to the graph

            if (flag)
                list2.RemoveAt(0);
            list2.Add(index, q2);

            // force redraw
            zedGraphControl2.Invalidate();
            zedGraphControl2.AxisChange();

            //Third Quaternion Redraw

            // Get the first CurveItem in the graph
            LineItem curve3 = zedGraphControl3.GraphPane.CurveList[0] as LineItem;

            // Get the PointPairList
            IPointListEdit list3 = curve3.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list3 == null)
                return;
            // add new data points to the graph

            if (flag)
                list3.RemoveAt(0);
            list3.Add(index, q3);

            // force redraw
            zedGraphControl3.Invalidate();
            zedGraphControl3.AxisChange();

            //Fourth Quaternion Redraw

            // Get the first CurveItem in the graph
            LineItem curve4 = zedGraphControl4.GraphPane.CurveList[0] as LineItem;

            // Get the PointPairList
            IPointListEdit list4 = curve4.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list4 == null)
                return;
            // add new data points to the graph
            if (flag)
                list4.RemoveAt(0);
            list4.Add(index, q4);

            // force redraw
            zedGraphControl4.Invalidate();
            zedGraphControl4.AxisChange();
        }

    }
}
