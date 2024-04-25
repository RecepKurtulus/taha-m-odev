using real_estate_app.Models;
using System;
using System.Data;
using System.Windows.Forms;


namespace real_estate_app
{
    public partial class Form1 : Form
    {
        AppDbContext db = new();

        

        public Form1()
        {
            InitializeComponent();

            List<Emlak> emlaklar = db.Emlaklar.ToList();
            EmlaklariListele(emlaklar);


        }
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaticiSoyisim_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaticiTelefon_TextChanged(object sender, EventArgs e)
        {

        }

        private void kayitOlButon_Click(object sender, EventArgs e)
        {
            string saticiIsim = SaticiIsim.Text;
            string saticiSoyisim= SaticiSoyisim.Text;
            string saticiTelNo = SaticiTelefon.Text;              
            if (saticiIsim != string.Empty && saticiSoyisim != string.Empty && saticiTelNo != string.Empty)
            {
                
                if (saticiTelNo.Length == 11)
                {
                    bool saticiVarMi = db.Saticilar.Any();
                    int sonSaticiId = saticiVarMi ? db.Saticilar.Max(s => s.SaticiId) : 0;
                    sonSaticiId++;
                    var satici = new Satici()
                    {
                        SaticiId = sonSaticiId,
                        Ad = saticiIsim,
                        Soyad = saticiSoyisim,
                        Telefon = saticiTelNo,

                    };
                    db.Saticilar.Add(satici);
                    db.SaveChanges();
                    MessageBox.Show("Hesap ba�ar�yla olu�turuldu.");
                }
                else
                {
                    MessageBox.Show("Telefon numaran�z� do�ru giriniz.");
                }

                
            }
            else
            {
                MessageBox.Show("L�tfen bo� alan b�rakmay�n.");
            }

           
            


        }


        private void SaticiPaneliButton_Click(object sender, EventArgs e)
        {
            
            KayitPanel.Visible = true;
            EmlakOlusturPanel.Visible = false;
            emlakListesiPanel.Visible = false;
        }

        private void EmlakKayitPaneliButon_Click(object sender, EventArgs e)

        {
            saticiListesiEmlakKayit.Items.Clear();
            foreach (Satici satici in db.Saticilar)
            {
                saticiListesiEmlakKayit.Items.Add(satici.Ad);
            }
            EmlakOlusturPanel.Visible=true;
            KayitPanel.Visible = false;
            emlakListesiPanel.Visible = false;
            


        }
        private void emlakListesiPanelButon_Click(object sender, EventArgs e)
        {
            saticiFiltre.Items.Clear();
            foreach (Satici satici in db.Saticilar)
            {
                saticiFiltre.Items.Add(satici.Ad);
            }
            KayitPanel.Visible = false;
            EmlakOlusturPanel.Visible = false;
            emlakListesiPanel.Visible = true;
        }

        private void emlakKaydetButon_Click(object sender, EventArgs e)
        {
            if (adresGirisi.Text!=null&&fiyatGirisi!=null&&metrekareGirisi!=null&&odaSayisiGirisi!=null&& saticiListesiEmlakKayit != null)
            {
                int saticiId = db.Saticilar.FirstOrDefault(s => s.Ad == saticiListesiEmlakKayit.Text).SaticiId;
                var emlak = new Emlak
                {
                    Adres = adresGirisi.Text,
                    Sehir = sehirGirisi.Text,
                    Ilce= ilceGirisi.Text,
                    OdaSayisi = Convert.ToInt32(odaSayisiGirisi.Text),
                    Fiyat = Convert.ToInt32(fiyatGirisi.Text),
                    MetreKare = Convert.ToInt32(metrekareGirisi.Text),
                    SaticiId = saticiId,
                };
                db.Emlaklar.Add(emlak);
                db.SaveChanges();
                
            }
        }

        private void filtreleButon_Click(object sender, EventArgs e)
        {
            List<Emlak> filtrelenmisEmlakListesi= new List<Emlak>();
            var emlakdb = db.Emlaklar;
            
            if (metreKareFiltre.Text.ToString()!=string.Empty)
            {
                foreach(Emlak emlak in emlakdb)
                {
                    if (emlak.MetreKare <= Convert.ToInt32(metreKareFiltre.Text)){
                        
                        filtrelenmisEmlakListesi.Add(emlak);
                        MessageBox.Show(emlak.ToString());
                    }
                   
                }
            }
            else
            {
                MessageBox.Show("Metrekare alan� bo�");
            }
           
            
            if(filtrelenmisEmlakListesi!= null)
            {
                filtrelenmisEmlakListesiUi.Rows.Clear();
                filtrelenmisEmlakListesiUi.Columns.Clear();
                //Sat�r ve s�tunlar� temizledik



                EmlaklariListele(filtrelenmisEmlakListesi);




            }
            
           
            




        }
        public void EmlaklariListele(List<Emlak> emlakListesi)
        {
            List<string> kolonIsimleriListe = new() { "Adres", "�ehir", "�l�e", "Oda Sayisi", "Metrekare", "Fiyat", "Sat�� Durumu", "Sat�c�" };
            foreach (string kolonIsmi in kolonIsimleriListe)
            {
                DataGridViewTextBoxColumn yeniKolon = new DataGridViewTextBoxColumn();
                yeniKolon.HeaderText = kolonIsmi;
                filtrelenmisEmlakListesiUi.Columns.Add(yeniKolon);
            }
            //S�tunlar� tek tek tan�mlad���m�z de�erler ile doldurduk
            foreach (Emlak emlak in emlakListesi)
            {
                string durum = emlak.SatildiMi ? "Sat�ld�" : "Sat��ta";
                Satici satici = db.Saticilar.FirstOrDefault(s => s.SaticiId == emlak.SaticiId);

                DataGridViewRow yeniSatir = new DataGridViewRow();
                // Emlak bilgilerini ilgili h�crelere yerle�tirdik
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.Adres });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.Sehir });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.Ilce });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.OdaSayisi });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.MetreKare });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.Fiyat });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = durum });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = $"{satici.Ad} {satici.Soyad}" });
                filtrelenmisEmlakListesiUi.Rows.Add(yeniSatir);
                

            }
           
            
        }
    }
}