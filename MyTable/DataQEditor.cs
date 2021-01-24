using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MyTable
{
    public class
        DataQEditor : UITypeEditor
    {

        /// <summary>/// Реализация метода редактирования/// </summary>
        public override Object EditValue(ITypeDescriptorContext context, IServiceProvider provider, Object value)
        {
            if ((context != null) && (provider != null))
            {
                IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (svc != null)
                {
                    using (SelectDTQ ipfrm = new SelectDTQ(new DateTimeQ(value.ToString())))
                    {
                        if (svc.ShowDialog(ipfrm) == DialogResult.OK)
                        {
                            value = ipfrm.DataQ;
                        }
                    }

                }
            }
            return base.EditValue(context, provider, value);
        }
        /// <summary>/// Возвращаем стиль редактора - модальное окно/// </summary>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null)
                return UITypeEditorEditStyle.Modal;
            else return base.GetEditStyle(context);
        }

    }
}
