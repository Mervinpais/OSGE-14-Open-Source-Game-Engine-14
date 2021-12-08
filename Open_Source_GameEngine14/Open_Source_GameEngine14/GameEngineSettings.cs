using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;

namespace Open_Source_GameEngine14
{
    public partial class GameEngineSettings : Form
    {
        public GameEngineSettings()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nice try looking for secrets, but this Picturebox has none :/","Nice try");
        }

        bool isSupportedLanguage = false;
        private void GameEngineSettings_Paint(object sender, PaintEventArgs e)
        {
            char quotes = '"';
            string filepathForLanguages = Environment.CurrentDirectory + @"\LanguageSettings.json";
            //Console.WriteLine(filepathForLanguages);
            if (File.Exists(filepathForLanguages))
            {
                //Console.WriteLine("======= FOUND LANGUAGES SETTINGS =============");
                string[] lines = File.ReadAllLines(filepathForLanguages);
                foreach (string line in lines)
                {
                    if (line.StartsWith("  " + quotes + "Language" + quotes))
                    {
                        string theLine = line;
                        theLine = theLine.Replace($"{quotes}Language{quotes}: {quotes}", "");
                        theLine = theLine.Replace(quotes.ToString(), "");
                        theLine = theLine.Replace(" ", "");
                        //Console.WriteLine(theLine);
                        if (theLine == "English")
                        {
                            isSupportedLanguage = true;
                            settingsTitle.Text = "Settings";
                            LanguageSettingsTitle.Text = "Language";
                            LanguageSettingsDescription.Text = "Go to Help >> Language and\n choose your language Example (Help >> Language >> Hindi)";
                            ColorModeTitle.Text = "Color Mode";
                            DarkModeOptionBTN.Text = "Dark Mode";
                            LightModeOptionBTN.Text = "Light Mode";
                        }
                        else if (theLine == "Hindi")
                        {
                            isSupportedLanguage = true;
                            settingsTitle.Text = "समायोजन";
                            LanguageSettingsTitle.Text = "भाषा";
                            LanguageSettingsDescription.Text = "सहायता >> भाषा और अपनी \n भाषा उदाहरण चुनें (सहायता >> भाषा >> हिंदी)";
                            ColorModeTitle.Text = "रंग मोड";
                            DarkModeOptionBTN.Text = "डार्क मोड";
                            LightModeOptionBTN.Text = "लाइट मोड";
                        }
                        else if (theLine == "Arabic")
                        {
                            isSupportedLanguage = true;
                            settingsTitle.Text = "إعدادات";
                            LanguageSettingsTitle.Text = "لغة";
                            LanguageSettingsDescription.Text = "انتقل إلى مساعدة >> اللغة و \n اختر مثال لغتك (المساعدة >> اللغة >> الهندية)";
                            ColorModeTitle.Text = "وضع اللون";
                            DarkModeOptionBTN.Text = "الوضع المظلم";
                            LightModeOptionBTN.Text = "وضع ضوء";
                        }
                        else if (theLine == "Spanish")
                        {
                            isSupportedLanguage = true;
                            settingsTitle.Text = "Ajustes";
                            LanguageSettingsTitle.Text = "Idioma";
                            LanguageSettingsDescription.Text = "Ir a ayudar >> Idioma y\n elegir su ejemplo de idioma (Ayuda >> Idioma >> Hindi)";
                            ColorModeTitle.Text = "Modo de color";
                            DarkModeOptionBTN.Text = "Modo oscuro";
                            LightModeOptionBTN.Text = "Modo de luz";
                        }
                        else if (theLine != "English" && theLine != "Hindi" && theLine != "Arabic" && theLine != "Spanish")
                        {
                            if (isSupportedLanguage == true)
                            {
                                MessageBox.Show($"A Language Error Ocurred at\n line 41 in GameEngineSettings.cs\n The Error;\n language {theLine} doesnt have an if statement to tell what text should be displayed(in the language {theLine})");
                            }
                            isSupportedLanguage = false;
                            settingsTitle.Text = "§èƭƭïñϱƨ";
                            LanguageSettingsTitle.Text = "£áñϱúáϱè";
                            LanguageSettingsDescription.Text = "Gô ƭô Hèℓƥ >> £áñϱúáϱè áñδ\n çλôôƨè ¥ôúř ℓáñϱúáϱè Éжá₥ƥℓè (Hèℓƥ >> £áñϱúáϱè >> Hïñδï)";
                            ColorModeTitle.Text = "Çôℓôř Môδè";
                            DarkModeOptionBTN.Text = "ÐářƙMôδè";
                            LightModeOptionBTN.Text = "£ïϱλƭ Môδè";
                        }
                    }
                }
            }
            else if (!File.Exists(filepathForLanguages))
            {
                Console.WriteLine("====DID NOT FOUND LANGUAGES SETTINGS =============");
                MessageBox.Show("LanguageSettings.json is missing or corrupted, please add/replace it to fix this error", "Missing File");
            }
        }
    }
}
