using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MDXInfo.Util.Script
{
    /// <summary>
    /// Edited version of control found at: www.mdxinfo.com/resources/scripting.php
    /// </summary>
    public class ScriptEditorControl : ScintillaNET.Scintilla
    {
        private ImageList _imageList;
        private IntellisenseListBox _intelliSenseBox;

        private Hashtable _variables = new Hashtable();
        private Hashtable _definedTypes = new Hashtable();

        private string _typedSincePeriod = string.Empty;

        private ReferencedAssemblyCollection _referencedAssemblies = new ReferencedAssemblyCollection();
        public ReferencedAssemblyCollection ReferencedAssemblies
        {
            get { return _referencedAssemblies; }
            set { _referencedAssemblies = value; }
        }

        public ScriptEditorControl()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += new EventHandler(ParseVariables);
                timer.Start();

                ReferencedAssemblies = new ReferencedAssemblyCollection();
                ReferencedAssemblies.ReferencedAssembliesChanged += new ReferencedAssembliesChanged(ReferencedAssemblies_ReferencedAssembliesChanged);
                ParseAssemblies();
            }
        }

        private void ReferencedAssemblies_ReferencedAssembliesChanged()
        {
            ParseAssemblies();
        }

        private void ParseVariables(object sender, EventArgs e)
        {
            try
            {
                _variables.Clear();
                string text = this.Text;

                foreach (string type in _definedTypes.Keys)
                {
                    string typeSearch = type + " ";
                    int searchLen = typeSearch.Length;

                    int index = text.IndexOf(typeSearch);

                    while (index > -1)
                    {
                        int spaceIndex = text.IndexOfAny(new char[] { ' ', ')', ';', '=' }, index + searchLen);
                        int varNameLen = spaceIndex - (index + searchLen);

                        if (spaceIndex > -1 && varNameLen > 0)
                        {
                            string varName = text.Substring(index + searchLen, varNameLen);

                            if (!_variables.ContainsKey(varName))
                            {
                                _variables.Add(varName, type);
                            }
                        }

                        index = text.IndexOf(typeSearch, index + 1);
                    }
                }
            }
            catch
            {
            }
        }

        private void InitializeComponent()
        {
            this._imageList = new ImageList();

            _imageList.ImageSize = new Size(16, 16);
            _imageList.ColorDepth = ColorDepth.Depth32Bit;
            _imageList.Images.Add(new Bitmap(this.GetType(), "Images.namespace.gif"));
            _imageList.Images.Add(new Bitmap(this.GetType(), "Images.class.gif"));
            _imageList.Images.Add(new Bitmap(this.GetType(), "Images.method.gif"));
            _imageList.Images.Add(new Bitmap(this.GetType(), "Images.property.gif"));
            _imageList.Images.Add(new Bitmap(this.GetType(), "Images.event.gif"));
            _imageList.Images.Add(new Bitmap(this.GetType(), "Images.interface.gif"));

            _intelliSenseBox = new IntellisenseListBox();
            _intelliSenseBox.ImageList = this._imageList;
            _intelliSenseBox.Size = new Size(300, 100);
            _intelliSenseBox.Visible = false;
            this.Controls.Add(_intelliSenseBox);

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBKeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TBMouseDown);
        }

        private void TBKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.OemPeriod)
            {
                if (!_intelliSenseBox.Visible)
                {
                    _typedSincePeriod = string.Empty;

                    string lastWord = GetLastWord();
                    int periodIndex = lastWord.IndexOf('.');

                    if (_variables.ContainsKey(lastWord))
                    {
                        // shortcut for variables

                        Type type = (Type)_definedTypes[_variables[lastWord]];
                        ShowIntellisenseBox(type);
                    }
                    else if (periodIndex != -1 && periodIndex != lastWord.Length - 1)
                    {
                        // since we have periods in our last works, let's see what we can do...
                        string[] tokens = lastWord.Split('.');

                        // if the first token is a variable of a known type
                        if (_variables.ContainsKey(tokens[0]))
                        {
                            // get known base type
                            Type type = (Type)_definedTypes[_variables[tokens[0]]];

                            for (int i = 1; i < tokens.Length; i++)
                            {
                                string token = tokens[i];

                                // chop off parentheses and parameters for methods
                                if (token.EndsWith(")"))
                                {
                                    token = token.Substring(0, token.IndexOf("("));
                                }

                                // get the member information for this token
                                MemberInfo[] mi = type.GetMember(token);

                                // extract type info for token
                                if (mi.Length > 0)
                                {
                                    switch (mi[0].MemberType)
                                    {
                                        case MemberTypes.Property:
                                            type = ((PropertyInfo)mi[0]).PropertyType;
                                            break;

                                        case MemberTypes.Method:
                                            type = ((MethodInfo)mi[0]).ReturnType;
                                            break;
                                    }
                                }

                                // break to prevent null ref on next iteration
                                if (type == null)
                                {
                                    break;
                                }
                            }

                            // if we actually found a type
                            if (type != null)
                            {
                                ShowIntellisenseBox(type);
                            }
                        }
                    }
                }
                else
                {
                    ConfirmIntellisenseBox();
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (_intelliSenseBox.Visible)
                {
                    if (_typedSincePeriod == string.Empty)
                    {
                        _intelliSenseBox.Visible = false;
                    }
                    else
                    {
                        _typedSincePeriod = _typedSincePeriod.Substring(0, _typedSincePeriod.Length - 1);

                        for (int i = 0; i < this._intelliSenseBox.Items.Count; i++)
                        {
                            if (this._intelliSenseBox.Items[i].ToString().ToLower().StartsWith(_typedSincePeriod.ToLower()))
                            {
                                _intelliSenseBox.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                // The up key moves up our member list, if
                // the list is visible
                if (_intelliSenseBox.Visible)
                {
                    if (_intelliSenseBox.SelectedIndex > 0)
                    {
                        _intelliSenseBox.SelectedIndex--;
                    }

                    e.Handled = true;
                    this.Focus();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (_intelliSenseBox.Visible)
                {
                    if (_intelliSenseBox.SelectedIndex < _intelliSenseBox.Items.Count - 1)
                    {
                        _intelliSenseBox.SelectedIndex++;
                    }

                    e.Handled = true;
                    this.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                if (_intelliSenseBox.Visible)
                {
                    if (_intelliSenseBox.SelectedIndex < _intelliSenseBox.Items.Count - 10)
                    {
                        _intelliSenseBox.SelectedIndex += 10;
                    }
                    else
                    {
                        _intelliSenseBox.SelectedIndex = _intelliSenseBox.Items.Count - 1;
                    }

                    e.Handled = true;
                    this.Focus();
                }
            }

            else if (e.KeyCode == Keys.PageUp)
            {
                if (_intelliSenseBox.Visible)
                {
                    if (_intelliSenseBox.SelectedIndex > 10)
                    {
                        _intelliSenseBox.SelectedIndex -= 10;
                    }
                    else
                    {
                        _intelliSenseBox.SelectedIndex = 0;
                    }

                    e.Handled = true;
                    this.Focus();
                }
            }
            else if (e.KeyCode == Keys.End)
            {
                if (_intelliSenseBox.Visible)
                {
                    _intelliSenseBox.SelectedIndex = _intelliSenseBox.Items.Count - 1;
                    e.Handled = true;
                    this.Focus();
                }
            }
            else if (e.KeyCode == Keys.Home)
            {
                if (_intelliSenseBox.Visible)
                {
                    _intelliSenseBox.SelectedIndex = 0;
                    e.Handled = true;
                    this.Focus();
                }
            }
            else if (e.KeyValue < 48 || (e.KeyValue >= 58 && e.KeyValue <= 64) || (e.KeyValue >= 91 && e.KeyValue <= 96) || e.KeyValue > 122)
            {
                // Check for any non alphanumerical key, hiding
                // member list box if it's visible.
                if (e.KeyCode == Keys.Return)
                {
                    if (_intelliSenseBox.Visible)
                    {
                        ConfirmIntellisenseBox();
                        e.Handled = true;
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    _intelliSenseBox.Visible = false;
                    _typedSincePeriod = string.Empty;
                }
            }
            else
            {
                // Letter or number typed, search for it in the listview
                if (_intelliSenseBox.Visible)
                {
                    _typedSincePeriod += (char)e.KeyValue;

                    for (int i = 0; i < this._intelliSenseBox.Items.Count; i++)
                    {
                        if (this._intelliSenseBox.Items[i].ToString().ToLower().StartsWith(_typedSincePeriod.ToLower()))
                        {
                            _intelliSenseBox.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    _typedSincePeriod = string.Empty;
                }
            }
        }

        public void ConfirmIntellisenseBox()
        {
            if (_typedSincePeriod != string.Empty)
            {
                int curPos = Selection.Start;
                Selection.Start = curPos - _typedSincePeriod.Length;
                Selection.Length = _typedSincePeriod.Length;

                DeleteSelection();
            }

            InsertAtCaret(_intelliSenseBox.SelectedItem.ToString());
            _intelliSenseBox.Visible = false;
            _typedSincePeriod = string.Empty;
            this.Focus();
        }

        private void DeleteSelection()
        {
            Clipboard.Cut();
        }

        private void ShowIntellisenseBox(Type type)
        {
            _intelliSenseBox.Populate(type);

            if (_intelliSenseBox.Items.Count > 0)
            {
                _intelliSenseBox.SelectedIndex = 0;
            }

            Point topLeft = base.Caret.PositionXY;
            topLeft.Offset(-35, 18);

            if (this.Size.Height < (topLeft.Y + _intelliSenseBox.Height))
            {
                topLeft.Offset(0, -18 - 18 - _intelliSenseBox.Height);
            }

            if (this.Size.Width < (topLeft.X + _intelliSenseBox.Width))
            {
                topLeft.Offset(35 + 15 - _intelliSenseBox.Width, 0);
            }

            if (topLeft.X < 0)
            {
                topLeft.X = 0;
            }

            if (topLeft.Y < 0)
            {
                topLeft.Y = 0;
            }

            _intelliSenseBox.Location = topLeft;

            _intelliSenseBox.Visible = true;
        }

        public void InsertAtCaret(string text)
        {
            System.Windows.Forms.Clipboard.SetText(text);
            Clipboard.Paste();
        }

        private void TBMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _intelliSenseBox.Visible = false;
        }

        private string GetLastWord()
        {
            return base.GetWordFromPosition(Selection.Start - 1);
        }

        private void ParseAssemblies()
        {
            _definedTypes.Clear();

            foreach (string assemblyName in ReferencedAssemblies)
            {
                try
                {
                    // So we'll just load the assemblies we can load from project files directly (no gac)
                    Assembly subjectAssembly = Assembly.LoadFrom(assemblyName);
                    Type[] assemblyTypes = subjectAssembly.GetExportedTypes();

                    // Cycle through types
                    foreach (Type type in assemblyTypes)
                    {
                        if (!_definedTypes.ContainsKey(type.Name))
                        {
                            // ignore new additions of this known type
                            _definedTypes.Add(type.Name, type);
                        }
                    }
                }
                catch (Exception exp)
                {
                    System.Diagnostics.Debug.WriteLine(exp);
                }
            }
        }
    }
}