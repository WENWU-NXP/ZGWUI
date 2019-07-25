using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZGWUI
{
    public partial class FlashProgram : Form
    {
        public FlashProgram()
        {
            InitializeComponent();
        }

        byte ChipVersion=0;
        public byte selectedChip
        {
            get
            {
                return ChipVersion;
            }
            set
            {
                ChipVersion = value;
                
            }
        }

        byte WhetherErase = 0;
        public byte whetherErase
        {
            get
            {
                return WhetherErase;
            }
            set
            {
                WhetherErase = value;

            }
        }


        private void buttonFLASHPROGRAMMEROK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonFLASHPROGRAMMERCANCEL_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void checkBoxFLASHPROGRAM5169_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFLASHPROGRAM5169.Checked == true)
            {
                ChipVersion = 1;
                checkBoxFLASHPROGRAM5189.Checked = false;
            }
            else
            {
                ChipVersion = 0;
            }

        }

        private void checkBoxFLASHPROGRAM5189_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFLASHPROGRAM5189.Checked == true)
            {
                ChipVersion = 2;
                checkBoxFLASHPROGRAM5169.Checked = false;
            }
            else
            {
                ChipVersion = 0;
            }
        }

        private void checkBoxFLASHPROGRAMERASE_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFLASHPROGRAMERASE.Checked == true)
            {
                WhetherErase = 1;
            }
            else
            {
                WhetherErase = 0;
            }

        }
    }
}
