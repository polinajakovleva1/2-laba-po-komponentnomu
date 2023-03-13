using PluginInterface;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MainApp
{
    public partial class PluginInfo : Form
    {
        public PluginInfo(Dictionary<string, IPlugin> plugins, List<Type> types)
        {
            InitializeComponent();

            int i = 0;
            foreach (KeyValuePair<string, IPlugin> plug in plugins)
            {
                object[] attrs = types[i].GetCustomAttributes(false);
                i++;
                listBox.Items.Add(plug.Key + ' ' + plug.Value.Author + ' ' + $"{((VersionAttribute)attrs[0]).Major}.{((VersionAttribute)attrs[0]).Minor}");
            }
        }
    }
}
