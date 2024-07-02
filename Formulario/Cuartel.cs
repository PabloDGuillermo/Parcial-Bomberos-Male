using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Formulario
{
    public partial class Cuartel2 : Form
    {
        private List<Bombero> bomberos;
        private List<PictureBox> fuegos;
        private CancellationTokenSource cancellationTokenSource;

        public Cuartel2()
        {
            InitializeComponent();

            fuegos = new List<PictureBox>();
            fuegos.Add(fuego1);
            fuegos.Add(fuego2);
            fuegos.Add(fuego3);
            fuegos.Add(fuego4);
            cancellationTokenSource = new CancellationTokenSource();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            bomberos = new List<Bombero>();
            Bombero b1 = new Bombero("M. Palermo");
            b1.MarcarFin += OcultarBombero;
            bomberos.Add(b1);
            Bombero b2 = new Bombero("G. Schelotto");
            b2.MarcarFin += OcultarBombero;
            bomberos.Add(b2);
            Bombero b3 = new Bombero("C. Tevez");
            b3.MarcarFin += OcultarBombero;
            bomberos.Add(b3);
            Bombero b4 = new Bombero("F. Gago");
            b4.MarcarFin += OcultarBombero;
            bomberos.Add(b4);
        }

        private void btnEnviar1_Click(object sender, EventArgs e)
        {
            EnviarBombero(0);
        }

        private void btnEnviar2_Click(object sender, EventArgs e)
        {
            EnviarBombero(1);
        }

        private void btnEnviar3_Click(object sender, EventArgs e)
        {
            EnviarBombero(2);
        }

        private void btnEnviar4_Click(object sender, EventArgs e)
        {
            EnviarBombero(3);
        }
        private void EnviarBombero(int index)
        {
            try
            {
                DespacharServicio(index);
            }
            catch (BomberoOcupadoException)
            {
                MessageBox.Show($"Salida del bombero {index} no concretada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DespacharServicio(int index)
        {
            Bombero bombero = bomberos[index];
            if(bombero.Salidas.Count != 0)
            {
                if (bombero.Salidas[bombero.Salidas.Count - 1].FechaFin == default)
                {
                    throw new BomberoOcupadoException();
                }
            } 
            fuegos[index].Visible = true;
            Task hilo = Task.Run(() => bombero.AtenderSalida(index), cancellationTokenSource.Token);
        }
        private void OcultarBombero(int bomberoIndex)
        {
            if (InvokeRequired)
            {
                Action<int> ocultarBombero = OcultarBombero;
                Invoke(ocultarBombero, bomberoIndex);
            }
            else
            {
                fuegos[bomberoIndex].Visible = false;
            }
        }

        private void Cuartel2_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }

        private void btnReporte1_Click(object sender, EventArgs e)
        {
           bomberos[0].Guardar(bomberos[0]);
        }

        private void btnReporte2_Click(object sender, EventArgs e)
        {
            bomberos[1].Guardar(bomberos[1]);
        }

        private void btnReporte3_Click(object sender, EventArgs e)
        {
            bomberos[2].Guardar(bomberos[2]);
        }

        private void btnReporte4_Click(object sender, EventArgs e)
        {
            bomberos[3].Guardar(bomberos[3]);
        }
    }
}
