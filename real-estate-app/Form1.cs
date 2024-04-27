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
        

       

        private void kayitOlButon_Click(object sender, EventArgs e)
        {
            string saticiIsim = SaticiIsim.Text;
            string saticiSoyisim= SaticiSoyisim.Text;
            string saticiTelNo = SaticiTelefon.Text;              
            if (saticiIsim != string.Empty && saticiIsim.All(char.IsLetter) && saticiSoyisim != string.Empty && saticiSoyisim.All(char.IsLetter) && saticiTelNo != string.Empty)
            {
                
                if (saticiTelNo.Length == 11&&saticiTelNo.All(char.IsDigit))
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
                    MessageBox.Show("Telefon numaran�z� do�ru formatta giriniz.");
                }

                
            }
            else
            {
                MessageBox.Show("Bo� alan b�rakmay�n�z ayr�ca girdi�iniz de�erlerin d�zg�n oldu�undan emin olunuz.");
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
                saticiFiltre.Items.Add($"{satici.Ad} {satici.Soyad}");
            }
            KayitPanel.Visible = false;
            EmlakOlusturPanel.Visible = false;
            emlakListesiPanel.Visible = true;
        }

        private void emlakKaydetButon_Click(object sender, EventArgs e)
        {
            if (adresGirisi.Text!=string.Empty&&fiyatGirisi.Text!= string.Empty&& fiyatGirisi.Text.All(char.IsDigit) && metrekareGirisi.Text!= string.Empty && metrekareGirisi.Text.All(char.IsDigit) && odaSayisiGirisi.Text!= string.Empty && odaSayisiGirisi.Text.All(char.IsDigit) && saticiListesiEmlakKayit !=null)
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
                MessageBox.Show($"{adresGirisi.Text} adresli emlak ba�ar�yla olu�turuldu.");
                
            }
            else
            {
                MessageBox.Show("L�tfen bo� alan b�rakmay�n�z ayr�ca t�m k�s�mlar�n do�ru formatta oldu�undan emin olunuz.");
            }
        }

        private void filtreleButon_Click(object sender, EventArgs e)
        {
           
            List<Emlak> filtrelenmisEmlakListesi= new List<Emlak>();
            var emlakdb = db.Emlaklar.ToList();
            bool metreKareVarMi, odaSayisiVarMi,fiyatVarMi,sehirVarMi,ilceVarMi,durumVarMi,saticiVarMi= false;
            metreKareVarMi = (metreKareFiltre.Text.ToString() != string.Empty);
            odaSayisiVarMi = (odaSayisiFiltre.Text.ToString() != string.Empty);
            fiyatVarMi = (fiyatFilteBaslangic.Text.ToString() != string.Empty && fiyatFiltreSon.Text.ToString() != string.Empty);
            saticiVarMi = (saticiFiltre.Text.ToString() != string.Empty);
            durumVarMi = (durumFiltre.Text.ToString() != string.Empty);
            sehirVarMi = (sehirFiltre.Text.ToString() != string.Empty);
            ilceVarMi = (ilceFiltre.Text.ToString() != string.Empty);
            // Yanl�� formatta veri girildi�inde kontrol
            if (metreKareVarMi && !int.TryParse(metreKareFiltre.Text, out _))
            {
                MessageBox.Show("L�tfen metrekare de�eri i�in do�ru formatta veri giriniz.");
                return;
            }

            if (fiyatVarMi && (!int.TryParse(fiyatFilteBaslangic.Text, out _) || !int.TryParse(fiyatFiltreSon.Text, out _)))
            {
                MessageBox.Show("L�tfen fiyat de�erleri i�in do�ru formatta veri giriniz.");
                return;
            }

            if (odaSayisiVarMi && !int.TryParse(odaSayisiFiltre.Text, out _))
            {
                MessageBox.Show("L�tfen oda say�s� i�in do�ru formatta veri giriniz.");
                return;
            }
            if (sehirVarMi && int.TryParse(sehirFiltre.Text, out _))
            {
                MessageBox.Show("L�tfen �ehir i�in do�ru formatta veri giriniz.");
                return;
            }
            if (ilceVarMi && int.TryParse(ilceFiltre.Text, out _))
            {
                MessageBox.Show("L�tfen il�e i�in do�ru formatta veri giriniz.");
                return;
            }

            foreach (Emlak emlak in emlakdb)
            {
                Satici satici = db.Saticilar.FirstOrDefault(s => s.SaticiId == emlak.SaticiId);
                string saticiIsim = $"{satici.Ad} {satici.Soyad}";
                string durum = (emlak.SatildiMi) ? "Sat�ld�" : "Sat��ta";
                if(metreKareVarMi&& emlak.MetreKare > Convert.ToInt32(metreKareFiltre.Text)){
                        continue;
                }
                
                if (fiyatVarMi &&(emlak.Fiyat > Convert.ToInt32(fiyatFiltreSon.Text) || emlak.Fiyat < Convert.ToInt32(fiyatFilteBaslangic.Text)))
                {
                    continue;
                }
                
                if (odaSayisiVarMi && emlak.OdaSayisi > Convert.ToInt32(odaSayisiFiltre.Text))
                {
                    continue;
                }
                if (saticiVarMi && saticiIsim != saticiFiltre.Text)
                {
                    continue;
                }
                if(durumVarMi&& durum != durumFiltre.Text)
                {
                    continue;
                }
                if (sehirVarMi && emlak.Sehir != sehirFiltre.Text)
                {
                    continue;
                }
                if (ilceVarMi && emlak.Ilce != ilceFiltre.Text)
                {
                    continue;
                }

                filtrelenmisEmlakListesi.Add(emlak);

            }
            
            


            if (filtrelenmisEmlakListesi!= null)
            {
                filtrelenmisEmlakListesiUi.Rows.Clear();
                filtrelenmisEmlakListesiUi.Columns.Clear();
                //Sat�r ve s�tunlar� temizledik



                EmlaklariListele(filtrelenmisEmlakListesi);




            }
            
           
            




        }
        public void EmlaklariListele(List<Emlak> emlakListesi)
        {
            List<string> kolonIsimleriListe = new() { "Emlak ID","Adres", "�ehir", "�l�e", "Oda Sayisi", "Metrekare", "Fiyat", "Sat�� Durumu", "Sat�c�","�lgili Nuamra"};
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
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.EmlakId });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.Adres });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.Sehir });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.Ilce });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.OdaSayisi });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.MetreKare });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = emlak.Fiyat });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = durum });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = $"{satici.Ad} {satici.Soyad}" });
                yeniSatir.Cells.Add(new DataGridViewTextBoxCell { Value = satici.Telefon});
                filtrelenmisEmlakListesiUi.Rows.Add(yeniSatir);
                

            }
           
            
        }

        public void Say�Mi(int degisken)
        {
            string degiskenString = degisken.ToString();
            bool sayiMi = int.TryParse(degiskenString, out _);
            if(!sayiMi)
            {
                MessageBox.Show("L�tfen say� de�eri giriniz.");
            }
        }

        private void emlakGuncelle_Click(object sender, EventArgs e)
        {
            if (int.TryParse(emlakIdDegistirmelik.Text, out _))
            {
                bool durum = (degistirmelikDurumlar.Text == "Sat�ld�") ? true : false;
                Emlak degistirilecekEmlak = db.Emlaklar.FirstOrDefault(e => e.EmlakId == int.Parse(emlakIdDegistirmelik.Text));
                if (degistirilecekEmlak != null)
                {
                    degistirilecekEmlak.SatildiMi = durum;
                    db.SaveChanges();
                    MessageBox.Show($"({degistirilecekEmlak.Adres}) adresli emla��n durumu ({degistirmelikDurumlar.Text}) olarak de�i�tirildi.");

                    filtrelenmisEmlakListesiUi.Rows.Clear();
                    filtrelenmisEmlakListesiUi.Columns.Clear();
                    EmlaklariListele(db.Emlaklar.ToList());
                }
                else
                {
                    MessageBox.Show($"{emlakIdDegistirmelik.Text} say�l� id ile ilgili bir emlak bulunmamaktad�r.");
                }
            }
            else
            {
                MessageBox.Show("ID alan�na do�ru formatta giri� yap�n�z.");
            }

        }

        private void emlakSilButon_Click(object sender, EventArgs e)
        {
            if(int.TryParse(emlakIdDegistirmelik.Text, out _))
            {
                Emlak silinecekEmlak = db.Emlaklar.FirstOrDefault(e => e.EmlakId == int.Parse(emlakIdDegistirmelik.Text));
                if (silinecekEmlak != null)
                {
                    db.Emlaklar.Remove(silinecekEmlak);
                    db.SaveChanges();
                    MessageBox.Show($"({silinecekEmlak.Adres}) adresli emlak silindi.");

                    filtrelenmisEmlakListesiUi.Rows.Clear();
                    filtrelenmisEmlakListesiUi.Columns.Clear();
                    EmlaklariListele(db.Emlaklar.ToList());
                }
                else
                {
                    MessageBox.Show($"{emlakIdDegistirmelik.Text} say�l� id ile ilgili bir emlak bulunmamaktad�r.");
                }
            }
            else
            {
                MessageBox.Show("ID alan�na do�ru formatta giri� yap�n�z.");
            }
            
            
        }
    }
}