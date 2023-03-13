using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MainApp
{
    public partial class PluginDemoApp : Form, BitApp
    {
        private readonly Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        List<Type> types = new List<Type>();
        public PluginDemoApp()
        {
            InitializeComponent();
            FindPlugins();
            CreatePluginsMenu();
        }
        public Bitmap Image
        {
            get => (Bitmap)pictureBox.Image;

            set => pictureBox.Image = value;
        }

        void FindPlugins()
        {
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;
            
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginInterface.IPlugin");
                        
                        if (iface != null)
                        {
                            types.Add(type);
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
            plugin.Transform(this);
        }

        private void CreatePluginsMenu()
        {
            foreach (var item in plugins)
            {
                var it = filters.DropDownItems.Add(item.Key);
                it.Click += OnPluginClick;
            }
        }

        private void pluginInfo_Click(object sender, EventArgs e)
        {
            PluginInfo plugForm = new PluginInfo(plugins, types);

            plugForm.ShowDialog(this);
        }
    }
}
