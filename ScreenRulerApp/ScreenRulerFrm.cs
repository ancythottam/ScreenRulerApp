using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRulerApp
{
    public partial class ScreenRulerFrm : Form
    {
        private Size size               = Size.Empty;
        private Point movePoint         = Point.Empty;
        private Pen pen                 = null;
        private StringFormat format     = null;
        private bool horizontal         = true;
        private bool isMoving           = false;
        private bool isLeftSizing       = false;
        private bool isRightSizing      = false;
        private bool isTopSizing        = false;
        private bool isBottomSizing     = false;

        private const int StepSize_Pixel                 = 5;
        private const int StepSize_Inch                  = 1;
        private const int StepSize_Centimeter            = 1;
        private const int MinRulerlimit_Pixel            = 10;
        private const int MinRulerlimit_Inch             = 2;
        private const int MinRulerlimit_Centimeter       = 5;
        private const int MaxRulerlimit_Pixel            = 50;
        private const int MaxRulerlimit_Inch             = 6;
        private const int MaxRulerlimit_Centimeter       = 10;
        private const int NoOfDivisions_Pixel            = 100;
        private const int NoOfDivisions_Inch             = 12;
        private const int NoOfDivisions_Centimeter       = 10;
        private const int ScaleIndex_Pixel               = 1;
        private const int ScaleIndex_Inch                = 12;
        private const int ScaleIndex_Centimeter          = 10;
        private const int RULEROFFSET                    = 3;
        private const int RULERMAXSTEPSIZE               = 10;
        private const int RULERMINSTEPSIZE               = 1;
        private const int MAXSTROKELIMIT                 = 3;
        private const int MINSTROKELIMIT                 = 2;
        private const float GraphicsPageScale_Inch       = 1f / 12f;
        private const float GraphicsPageScale_Centimeter = 1f;
        private const string PIXEL_SCALEUNIT             = " Pixels";
        private const string INCH_SCALEUNIT              = " Inches";
        private const string CENTIMETER_SCALEUNIT        = " Cm";

        
        public ScreenRulerFrm()
        {
            InitializeComponent();
            this.size = new Size(this.Width, this.Width);
            this.pen = new Pen(Color.Black, float.Epsilon);
            this.format = new StringFormat(StringFormat.GenericTypographic);
            this.format.FormatFlags = StringFormatFlags.NoWrap;
            this.format.Trimming = StringTrimming.Character;
        }

       /// <summary>
       /// Form load event
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void OnRuler_Load(object sender, EventArgs e)
        {
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Horizontal = true;
        }

        private bool Horizontal
        {
            get 
            { 
                return this.horizontal; 
            }
            set
            {
                this.horizontal = value;
                if (this.horizontal)
                {
                    this.Size = new Size(this.size.Width, this.MinimumSize.Height);
                }
                else
                {
                    this.Size = new Size(this.MinimumSize.Width, this.size.Height);
                }
            }
        }

        /// <summary>
        /// menuItemFlip click event handler
        /// </summary>
        /// <param name="sender"></param>
        // <param name="e"></param>
        private void OnMenuItemFlipClick(object sender, EventArgs e)
        {
            this.Horizontal = !this.Horizontal;
            this.Invalidate();
        }

        /// <summary>
        /// menuItemPixel click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMenuItemPixel_Click(object sender, EventArgs e)
        {
            this.menuItemPixel.Checked      = true;
            this.menuItemInch.Checked       = false;
            this.menuItemCentimeter.Checked = false;
            this.Invalidate();
        }

        /// <summary>
        /// menuItemInch click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMenuItemInch_Click(object sender, EventArgs e)
        {
            this.menuItemPixel.Checked      = false;
            this.menuItemInch.Checked       = true;
            this.menuItemCentimeter.Checked = false;
            this.Invalidate();
        }

        /// <summary>
        /// menuItemCentimeter click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMenuItemCentimeter_Click(object sender, EventArgs e)
        {
            this.menuItemPixel.Checked      = false;
            this.menuItemInch.Checked       = false;
            this.menuItemCentimeter.Checked = true;
            this.Invalidate();
        }

        /// <summary>
        /// menuItemExit click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Form key down event handler
        /// Handles ruler resizing on key press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRuler_KeyDown(object sender, KeyEventArgs e)
        {
            int rulerStep  = e.Shift ? RULERMAXSTEPSIZE : RULERMINSTEPSIZE;
            switch(e.KeyCode)
            {
                case Keys.Left:
                     if (e.Control && this.Horizontal)
                {
                    this.Width      -=  rulerStep;
                    this.size.Width  =   this.Width;
                }
                else
                {
                    this.Location = new Point(this.Location.X - rulerStep, this.Location.Y);
                }
                this.Invalidate();
                break;

                case Keys.Right:
                if (e.Control && this.Horizontal)
                {
                    this.Width      += rulerStep;
                    this.size.Width = this.Width;
                }
                else
                {
                    this.Location = new Point(this.Location.X + rulerStep, this.Location.Y);
                }
                this.Invalidate();
                break;

                case Keys.Up:
                if (e.Control && !this.Horizontal)
                {
                    this.Height -= rulerStep;
                    this.size.Height = this.Height;
                }
                else
                {
                    this.Location = new Point(this.Location.X, this.Location.Y - rulerStep);
                }
                this.Invalidate();
                break;

                case Keys.Down:
                if (e.Control && !this.Horizontal)
                {
                    this.Height += rulerStep;
                    this.size.Height = this.Height;
                }
                else
                {
                    this.Location = new Point(this.Location.X, this.Location.Y + rulerStep);
                }
                this.Invalidate();
                break;
            }
        }

        /// <summary>
        /// Form mousedown event handler.
        /// Updates the flags for handling ruler resizing 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRuler_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks <= 1)
            {
                this.Capture  =  true;
                if (this.Horizontal)
                {
                    if (e.X <= this.Bounds.X)
                    {
                        this.isLeftSizing   = true;
                    }

                    else if (e.X >= this.Width - RULEROFFSET)
                    {
                        this.isRightSizing  = true;
                    }

                    else
                    {
                        this.isMoving       = true;
                    }
                }
                else
                {
                    if (e.Y <= RULEROFFSET)
                    {
                        this.isTopSizing    = true;
                    }
                    else if (e.Y >= this.Height - RULEROFFSET)
                    {
                        this.isBottomSizing = true;
                    }
                    else
                    {
                        this.isMoving       = true;
                    }
                }
                this.movePoint = this.PointToScreen(new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// Form mouse move event handler
        /// Handles ruler resizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRuler_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Capture)
            {
                Point pointToScreen    =   this.PointToScreen(new Point(e.X, e.Y));
                Rectangle rulerBounds  =   this.Bounds;
                Size rulerMinsize      =   this.MinimumSize;

                if (this.isMoving)
                {
                    this.Location   =  new Point(
                        rulerBounds.X + pointToScreen.X - this.movePoint.X,
                        rulerBounds.Y + pointToScreen.Y - this.movePoint.Y
                    );
                }

                else if (this.isLeftSizing)
                {
                    this.Bounds  =  new Rectangle(
                        rulerBounds.X + pointToScreen.X - this.movePoint.X,
                        rulerBounds.Y,
                        rulerBounds.Width - pointToScreen.X + this.movePoint.X,
                        rulerMinsize.Height
                    );
                    this.size.Width = this.Width;
                }

                else if (this.isRightSizing)
                {
                    this.Size = new Size(rulerBounds.Width + pointToScreen.X - this.movePoint.X, rulerBounds.Height);
                    this.size.Width = this.Width;
                }

                else if (this.isTopSizing)
                {
                    this.Bounds = new Rectangle(
                        rulerBounds.X,
                        rulerBounds.Y + pointToScreen.Y - this.movePoint.Y,
                        rulerMinsize.Width,
                        rulerBounds.Height - pointToScreen.Y + this.movePoint.Y
                    );
                    this.size.Height = this.Height;
                }

                else if (this.isBottomSizing)
                {
                    this.Size = new Size(rulerMinsize.Width, rulerBounds.Height + pointToScreen.Y - this.movePoint.Y);
                    this.size.Height = this.Height;
                }
                this.movePoint = pointToScreen;
            }

            else
            {
                if (this.Horizontal)
                {
                    if (e.X <= RULEROFFSET || e.X >= this.Width - RULEROFFSET)
                    {
                        this.Cursor = Cursors.SizeWE;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    if (e.Y <= RULEROFFSET || e.Y >= this.Height - RULEROFFSET)
                    {
                        this.Cursor = Cursors.SizeNS;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        /// <summary>
        /// Form mouseup event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRuler_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.Capture && e.Button == MouseButtons.Left)
            {
                this.isMoving       =
                this.isLeftSizing   =
                this.isRightSizing  =
                this.isTopSizing    =
                this.isBottomSizing =
                this.Capture        =   false;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Form paint event handler. 
        /// Draws the Ruler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRuler_Paint(object sender, PaintEventArgs e)
        {
        
            Graphics graphicsRuler  = e.Graphics;
			int scaleIndex              = 0;
			int stepSize                = 0;
			int minRulerLimit           = 0;
			int maxRulerLimt            = 0;
			int noOfRulerdivisions      = 0;
			string scaleUnit            = string.Empty;

            foreach (ToolStripMenuItem menuItem in contextMenuStrip.Items)
            {
                if (menuItem.Checked)
                {
                    switch (menuItem.Name)
                   {
                        case "menuItemPixel":
                           stepSize             = StepSize_Pixel;
                           minRulerLimit        = MinRulerlimit_Pixel;
                           maxRulerLimt         = MaxRulerlimit_Pixel;
                           noOfRulerdivisions   = NoOfDivisions_Pixel;
                           scaleIndex           = ScaleIndex_Pixel;
                           scaleUnit            = PIXEL_SCALEUNIT;
                           break;

                        case "menuItemInch":
                           graphicsRuler.PageUnit     = GraphicsUnit.Inch;
                           graphicsRuler.PageScale    = GraphicsPageScale_Inch;
                           stepSize             = StepSize_Inch;
                           minRulerLimit        = MinRulerlimit_Inch;
                           maxRulerLimt         = MaxRulerlimit_Inch;
                           noOfRulerdivisions   = NoOfDivisions_Inch;
                           scaleIndex           = ScaleIndex_Inch;
                           scaleUnit            = INCH_SCALEUNIT;
                           break;

                        case "menuItemCentimeter":
                           graphicsRuler.PageUnit    = GraphicsUnit.Millimeter;
                           graphicsRuler.PageScale = GraphicsPageScale_Centimeter;
                           stepSize             = StepSize_Centimeter;
                           minRulerLimit        = MinRulerlimit_Centimeter;
                           maxRulerLimt         = MaxRulerlimit_Centimeter;
                           noOfRulerdivisions   = NoOfDivisions_Centimeter;
                           scaleIndex           = ScaleIndex_Centimeter;
                           scaleUnit            = CENTIMETER_SCALEUNIT;
                           break;

                   }
                }
            }
			
            //Initializing the Ruler reference point array
			PointF[] refPoint = new PointF[] {
				new PointF(2, 2), new PointF(5, 5), new Point(this.Size), this.Location
			};
            graphicsRuler.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, refPoint);
            float infoDelta = this.Horizontal ? refPoint[0].Y : refPoint[0].X;
            float rulerStroke = this.Horizontal ? refPoint[1].Y : refPoint[1].X;
            int rulerLength = (int)(refPoint[2].X + refPoint[2].Y);

			if(!this.Horizontal) 
            {
                graphicsRuler.RotateTransform(90, MatrixOrder.Prepend);
                graphicsRuler.TranslateTransform(refPoint[2].X, 0, MatrixOrder.Append);
			}

            for (int i = 0; i < rulerLength; i += stepSize)
            {
				float strokeIndex = 1;
				if(i % minRulerLimit == 0) {
					if(i % maxRulerLimt == 0) {
                        strokeIndex = MAXSTROKELIMIT;
					} else {
                        strokeIndex = MINSTROKELIMIT;
					}
				}
                graphicsRuler.DrawLine(this.pen, i, 0f, i, strokeIndex * rulerStroke);
				if((i % noOfRulerdivisions) == 0) {
					string text = (i / scaleIndex).ToString(CultureInfo.InvariantCulture);
                    SizeF size = graphicsRuler.MeasureString(text, this.Font, rulerLength, this.format);
                    graphicsRuler.DrawString(text, this.Font, Brushes.Black, i - size.Width / 2, strokeIndex * rulerStroke, this.format);
				}
			}
			string info = string.Format(CultureInfo.InvariantCulture,
				"X={0} Y={1} Length={2}{3}",
                Math.Round(refPoint[3].X / scaleIndex, 1),
                Math.Round(refPoint[3].Y / scaleIndex, 1),
                Math.Round((float)(this.Horizontal ? refPoint[2].X : refPoint[2].Y) / scaleIndex, 1),
				scaleUnit
			);
            SizeF infoSize = graphicsRuler.MeasureString(info, this.Font, rulerLength, this.format);
            float y = (float)(this.Horizontal ? refPoint[2].Y : refPoint[2].X);
            graphicsRuler.DrawString(info, this.Font, Brushes.Black,
				(y - infoSize.Height) / 2, y - infoSize.Height - infoDelta, this.format
			);
        }
       
    }
}
