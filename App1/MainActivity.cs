using Android.App;
using Android.Widget;
using Android.OS;
using System.Linq;
using Android.Views;
using System.Windows;



namespace App1
{


    [Activity(Label = "ZerspanerFuchs", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        int count = 0;
        readonly Rechnen Formel = new Rechnen() ;

        private void ResetClick(View view)
        {

        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            
            Button button = FindViewById<Button>(Resource.Id.button1);
            Button btn2 = FindViewById<Button>(Resource.Id.button2);
            var label = FindViewById<TextView>(Resource.Id.textView4);     
            EditText toolDm = FindViewById<EditText>(Resource.Id.eT_toolDm );
            EditText cutSpd = FindViewById<EditText>(Resource.Id.eT_cutSpd );
            EditText splSpd = FindViewById<EditText>(Resource.Id.eT_splSpd );
            EditText toothFeed = FindViewById<EditText>(Resource.Id.eT_toothFeed);
            EditText totalFeed = FindViewById<EditText>(Resource.Id.eT_totalFeed);
            EditText cutWidth = FindViewById<EditText>(Resource.Id.eT_cutWidth);
            EditText toothNbr = FindViewById<EditText>(Resource.Id.eT_toothNbr);
            EditText cutDepth = FindViewById<EditText>(Resource.Id.eT_cutDepth);
            EditText zeitspan = FindViewById<EditText>(Resource.Id.editText7);
            EditText maxChipThickness = FindViewById<EditText>(Resource.Id.eT_maxChipThickness);
            EditText medChipThickness = FindViewById<EditText>(Resource.Id.eT_medChipThickness);
            EditText forcedVc = FindViewById<EditText>(Resource.Id.eT_forcedVc);
            EditText drall = FindViewById<EditText>(Resource.Id.editText14);
            //zeitspan.Enabled = false;

            

            drall.KeyPress += (object sender, View.KeyEventArgs e) =>
            {
                e.Handled = false;
                count = 0;
                button.Text = "Reset";
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    if (drall.Text.Contains("."))
                    {
                        drall.Text = drall.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                    }
                    // Toast.MakeText(this, eT_toolDm.Text, ToastLength.Short).Show();
                    if (double.TryParse(drall.Text, out double dr))
                    {
                        Formel.Drallwinkel = dr;
                        Toast.MakeText(this, Formel.Einstellwinkel.ToString(), ToastLength.Short).Show();
                    }
                    e.Handled = true;
                }

            };

            toolDm.FocusChange += delegate
            {

                count = 0;
                button.Text = "Reset";
                if (toolDm.Text.Contains("."))
                {
                    toolDm.Text = toolDm.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                }
                // Toast.MakeText(this, eT_toolDm.Text, ToastLength.Short).Show();
                if (double.TryParse(toolDm.Text, out double wkz))
                {
                    Formel.Werkzeugdurchmesser = wkz;
                    if (Formel.Schnittgeschwindigkeit != 0)
                    {
                        splSpd.Text = Formel.Spindeldrehzahl.ToString();
                    }
                    if (Formel.Eingriffsbreite > 0)
                    {
                        forcedVc.Text = Formel.VcFaktor.ToString();
                    }
                }
            };


            //eT_toolDm.KeyPress += (object sender, View.KeyEventArgs e) =>
            // {
            //     double wkz;
            //     e.Handled = false;
            //     count = 0;
            //     button.Text = "Reset";
            //     if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //     {
            //         if (eT_toolDm.Text.Contains("."))
            //         {
            //             eT_toolDm.Text = eT_toolDm.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            //         }
            //         // Toast.MakeText(this, eT_toolDm.Text, ToastLength.Short).Show();
            //         if (double.TryParse(eT_toolDm.Text, out wkz))                        
            //         {
            //             Formel.Werkzeugdurchmesser = wkz;
            //             if (Formel.Schnittgeschwindigkeit  != 0)
            //             {
            //                 eT_splSpd.Text = Formel.Spindeldrehzahl.ToString();
            //             }
            //             if (Formel.Eingriffsbreite  > 0)
            //             {
            //                 forcedVc.Text = Formel.VcFaktor.ToString();
            //             }
            //         }
            //        e.Handled = true;
            //     }
            // };

            cutSpd.FocusChange += delegate
            {

                count = 0;
                button.Text = "Reset";

                if (cutSpd.Text.Contains("."))
                {
                    cutSpd.Text = cutSpd.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                }
                // Toast.MakeText(this, eT_cutSpd.Text, ToastLength.Short).Show();
                if (double.TryParse(cutSpd.Text, out double cut))
                {
                    Formel.Schnittgeschwindigkeit = cut;
                    if (Formel.Werkzeugdurchmesser != 0)
                    {
                        splSpd.Text = Formel.Spindeldrehzahl.ToString();
                        if (Formel.Zahnvorschub != 0 && Formel.Anzahlschneiden != 0)
                        {
                            totalFeed.Text = Formel.Vorschub.ToString();
                            if (Formel.Eingriffsbreite != 0 && Formel.Zustelltiefe != 0)
                            {
                                zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                            }
                        }
                    }
                }

            };
            

            //eT_cutSpd.KeyPress += (object sender, View.KeyEventArgs e) =>
            //{
            //    double cut;
            //    e.Handled = false;
            //    count = 0;
            //    button.Text = "Reset";
            //   if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        if (eT_cutSpd.Text.Contains("."))
            //        {
            //            eT_cutSpd.Text = eT_cutSpd.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            //        }
            //        // Toast.MakeText(this, eT_cutSpd.Text, ToastLength.Short).Show();
            //        if (double.TryParse(eT_cutSpd.Text, out cut))
            //        {
            //            Formel.Schnittgeschwindigkeit = cut;
            //            if (Formel.Werkzeugdurchmesser  != 0)
            //            {
            //                eT_splSpd.Text = Formel.Spindeldrehzahl.ToString();
            //                if (Formel.Zahnvorschub != 0 && Formel.Anzahlschneiden != 0)
            //                {
            //                    totalFeed.Text = Formel.Vorschub.ToString();
            //                    if (Formel.Eingriffsbreite != 0 && Formel.Zustelltiefe != 0)
            //                    {
            //                        zeitspan.Text = Formel.Zeitspanvolumen.ToString();
            //                    }
            //                }
            //            }
            //        }
            //        e.Handled = true;
            //    }
            //};


            splSpd.FocusChange  += delegate
            {
                count = 0;
                button.Text = "Reset";
                if (splSpd.Text.Contains("."))
                {
                    splSpd.Text = splSpd.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                }
                // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
                if (double.TryParse(splSpd.Text, out double spl))
                    {
                        Formel.Spindeldrehzahl = spl;
                        if (Formel.Werkzeugdurchmesser != 0)
                        {
                            cutSpd.Text = Formel.Schnittgeschwindigkeit.ToString();  
                            if (Formel.Zahnvorschub != 0 && Formel.Anzahlschneiden != 0)
                            {
                                totalFeed.Text = Formel.Vorschub.ToString();
                                zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                                
                                if (Formel.Eingriffsbreite != 0 && Formel.Zahnvorschub != 0 && Formel.Werkzeugdurchmesser != 0)
                                {
                                    medChipThickness.Text = Formel.Mittenspandicke.ToString();
                                    maxChipThickness.Text = Formel.Maximalspandicke.ToString();
                                 }
                            }
                        }
                    }                           
            };

            //eT_splSpd.KeyPress += (object sender, View.KeyEventArgs e) =>
            //{
            //    double spl;
            //    e.Handled = false;
            //    count = 0;
            //    button.Text = "Reset";
            //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        if (eT_splSpd.Text.Contains("."))
            //        {
            //            eT_splSpd.Text = eT_splSpd.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            //        }
            //        // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
            //        if (double.TryParse(eT_splSpd.Text, out spl))
            //        {
            //            Formel.Spindeldrehzahl = spl;
            //            if (Formel.Werkzeugdurchmesser != 0)
            //            {
            //                eT_cutSpd.Text = Formel.Schnittgeschwindigkeit.ToString();
            //                if (Formel.Zahnvorschub != 0 && Formel.Anzahlschneiden != 0)
            //                {
            //                    totalFeed.Text = Formel.Vorschub.ToString();
            //                    zeitspan.Text = Formel.Zeitspanvolumen.ToString();

            //                    if (Formel.Eingriffsbreite != 0 && Formel.Zahnvorschub != 0 && Formel.Werkzeugdurchmesser != 0)
            //                    {
            //                        medChipThickness.Text = Formel.Mittenspandicke.ToString();
            //                        maxChipThickness.Text = Formel.Maximalspandicke.ToString();
            //                    }
            //                }
            //            }
            //        }
            //        e.Handled = true;
            //    }
            //};


            toothNbr.FocusChange+=delegate 
            {
                count = 0;
                button.Text = "Reset";
                    // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
                    if (int.TryParse(toothNbr .Text, out int i))
                    {
                        Formel.Anzahlschneiden = i;
                        if (Formel.Spindeldrehzahl != 0)
                        {
                            totalFeed.Text = Formel.Vorschub.ToString();
                            zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                            if (Formel.Eingriffsbreite != 0 && Formel.Zahnvorschub != 0 && Formel.Werkzeugdurchmesser != 0)
                            {
                                medChipThickness.Text = Formel.Mittenspandicke.ToString();
                                maxChipThickness.Text = Formel.Maximalspandicke.ToString();
                            }
                        }
                    }
            };

            //toothNbr.KeyPress += (object sender, View.KeyEventArgs e) =>
            //{
            //    int i;
            //    e.Handled = false;
            //    count = 0;
            //    button.Text = "Reset";
            //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
            //        if (int.TryParse(toothNbr.Text, out i))
            //        {
            //            Formel.Anzahlschneiden = i;
            //            if (Formel.Spindeldrehzahl != 0)
            //            {
            //                totalFeed.Text = Formel.Vorschub.ToString();
            //                zeitspan.Text = Formel.Zeitspanvolumen.ToString();
            //                if (Formel.Eingriffsbreite != 0 && Formel.Zahnvorschub != 0 && Formel.Werkzeugdurchmesser != 0)
            //                {
            //                    medChipThickness.Text = Formel.Mittenspandicke.ToString();
            //                    maxChipThickness.Text = Formel.Maximalspandicke.ToString();
            //                }
            //            }
            //        }
            //        e.Handled = true;
            //    }
            //};


            toothFeed.FocusChange+=delegate 
            {

                count = 0;
                button.Text = "Reset";
                    // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
                if (toothFeed.Text.Contains("."))
                    {
                        toothFeed.Text = toothFeed.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                    }
                    
                if (double.TryParse(toothFeed.Text, out double zv))
                    {
                        Formel.Zahnvorschub = zv;
                        if (Formel.Spindeldrehzahl != 0)
                        {
                            totalFeed.Text = Formel.Vorschub.ToString();
                            zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                            if (Formel.Eingriffsbreite != 0 && Formel.Zahnvorschub != 0 && Formel.Werkzeugdurchmesser != 0)
                            {
                                medChipThickness.Text = Formel.Mittenspandicke.ToString();
                                maxChipThickness.Text = Formel.Maximalspandicke.ToString();
                            }
                        }
                }
            };

            //toothFeed.KeyPress += (object sender, View.KeyEventArgs e) =>
            //{
            //    double zv;
            //    e.Handled = false;
            //    count = 0;
            //    button.Text = "Reset";
            //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
            //        if (toothFeed.Text.Contains("."))
            //        {
            //            toothFeed.Text = toothFeed.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            //        }

            //        if (double.TryParse(toothFeed.Text, out zv))
            //        {
            //            Formel.Zahnvorschub = zv;
            //            if (Formel.Spindeldrehzahl != 0)
            //            {
            //                totalFeed.Text = Formel.Vorschub.ToString();
            //                zeitspan.Text = Formel.Zeitspanvolumen.ToString();
            //                if (Formel.Eingriffsbreite != 0 && Formel.Zahnvorschub != 0 && Formel.Werkzeugdurchmesser != 0)
            //                {
            //                    medChipThickness.Text = Formel.Mittenspandicke.ToString();
            //                    maxChipThickness.Text = Formel.Maximalspandicke.ToString();
            //                }
            //            }
            //        }
            //        e.Handled = true;
            //    }
            //};

            totalFeed.FocusChange += delegate
            {
                double v;
                count = 0;
                button.Text = "Reset";
                    if (totalFeed.Text.Contains("."))
                    {
                        totalFeed.Text = totalFeed.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                    }
                    // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
                    if (double.TryParse(totalFeed.Text, out v))
                    {
                        Formel.Vorschub = v;
                        if (Formel.Spindeldrehzahl != 0)
                        {
                            if (Formel.Anzahlschneiden != 0)
                            {
                                toothFeed.Text = Formel.Zahnvorschub.ToString();
                            }
                            zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                            if (Formel.Eingriffsbreite != 0 && Formel.Zahnvorschub != 0 && Formel.Werkzeugdurchmesser != 0)
                            {
                                medChipThickness.Text = Formel.Mittenspandicke.ToString();
                                maxChipThickness.Text = Formel.Maximalspandicke.ToString();
                            }
                        }
                    }

            };

            //totalFeed.KeyPress += (object sender, View.KeyEventArgs e) =>
            //{
            //    double v;
            //    e.Handled = false;
            //    count = 0;
            //    button.Text = "Reset";
            //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        if (totalFeed.Text.Contains("."))
            //        {
            //            totalFeed.Text = totalFeed.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            //        }
            //        // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
            //        if (double.TryParse(totalFeed.Text, out v))
            //        {
            //            Formel.Vorschub = v;
            //            if (Formel.Spindeldrehzahl != 0)
            //            {
            //                if (Formel.Anzahlschneiden != 0)
            //                {
            //                    toothFeed.Text = Formel.Zahnvorschub.ToString();
            //                }
            //                zeitspan.Text = Formel.Zeitspanvolumen.ToString();
            //                if (Formel.Eingriffsbreite != 0 && Formel.Zahnvorschub != 0 && Formel.Werkzeugdurchmesser != 0)
            //                {
            //                    medChipThickness.Text = Formel.Mittenspandicke.ToString();
            //                    maxChipThickness.Text = Formel.Maximalspandicke.ToString();
            //                }
            //            }
            //        }
            //        e.Handled = true;
            //    }
            //};

            cutWidth.FocusChange += delegate 
            {
                double eb;
                count = 0;
                button.Text = "Reset";
                    if (cutWidth.Text.Contains("."))
                    {
                        cutWidth.Text = cutWidth.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                    }
                    // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
                    if (double.TryParse(cutWidth.Text, out eb))
                    {
                        Formel.Eingriffsbreite = eb;
                        zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                        forcedVc.Text = Formel.VcFaktor.ToString();
                        medChipThickness.Text = Formel.Mittenspandicke.ToString();
                        maxChipThickness.Text = Formel.Maximalspandicke.ToString();
                    //   Toast.MakeText(this, Formel.Eingriffswinkel.ToString(), ToastLength.Short).Show();
                        Toast.MakeText(this, Formel.Gesamtschneidenlaenge.ToString(), ToastLength.Short).Show();

                }
            };

            //cutWidth.KeyPress += (object sender, View.KeyEventArgs e) =>
            //{
            //    double eb;
            //    e.Handled = false;
            //    count = 0;
            //    button.Text = "Reset";
            //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        if (cutWidth.Text.Contains("."))
            //        {
            //            cutWidth.Text = cutWidth.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            //        }
            //        // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
            //        if (double.TryParse(cutWidth.Text, out eb))
            //        {
            //            Formel.Eingriffsbreite = eb;
            //            zeitspan.Text = Formel.Zeitspanvolumen.ToString();
            //            forcedVc.Text = Formel.VcFaktor.ToString();
            //            medChipThickness.Text = Formel.Mittenspandicke.ToString();
            //            maxChipThickness.Text = Formel.Maximalspandicke.ToString();
            //            Toast.MakeText(this, Formel.Eingriffswinkel.ToString(), ToastLength.Short).Show();
            //        }
            //        e.Handled = true;
            //    }
            //};

            cutDepth.FocusChange += delegate
            {
                double zs;
                count = 0;
                button.Text = "Reset";
                    if (cutDepth.Text.Contains("."))
                    {
                        cutDepth.Text = cutDepth.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                    }
                    // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
                    if (double.TryParse(cutDepth.Text, out zs))
                    {
                        Formel.Zustelltiefe = zs;
                        zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                    Toast.MakeText(this, Formel.Schneideneingriffslaengereal.ToString(), ToastLength.Short).Show();

                    }
            };

            //cutDepth.KeyPress += (object sender, View.KeyEventArgs e) =>
            //{
            //    double zs;
            //    e.Handled = false;
            //    count = 0;
            //    button.Text = "Reset";
            //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        if (cutDepth.Text.Contains("."))
            //        {
            //            cutDepth.Text = cutDepth.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            //        }
            //        // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
            //        if (double.TryParse(cutDepth.Text, out zs))
            //        {
            //            Formel.Zustelltiefe = zs;
            //            zeitspan.Text = Formel.Zeitspanvolumen.ToString();
            //        }
            //        e.Handled = true;
            //    }
            //};

            maxChipThickness.FocusChange += delegate 
            {
                double mw;
                count = 0;
                button.Text = "Reset";
                    // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
                    if (maxChipThickness.Text.Contains("."))
                    {
                        maxChipThickness.Text = maxChipThickness.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                    }
                    if (double.TryParse(maxChipThickness.Text, out mw))
                    {
                        Formel.Maximalspandicke = mw;
                        toothFeed.Text = Formel.Zahnvorschub.ToString();
                        totalFeed.Text = Formel.Vorschub.ToString();
                        medChipThickness.Text = Formel.Mittenspandicke.ToString();
                        zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                    Toast.MakeText(this, Formel.ASIE.ToString(), ToastLength.Short).Show();

                }
            };

            //maxChipThickness.KeyPress += (object sender, View.KeyEventArgs e) =>
            //{
            //    double mw;
            //    e.Handled = false;
            //    count = 0;
            //    button.Text = "Reset";
            //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
            //        if (maxChipThickness.Text.Contains("."))
            //        {
            //            maxChipThickness.Text = maxChipThickness.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            //        }
            //        if (double.TryParse(maxChipThickness.Text, out mw))
            //        {
            //            Formel.Maximalspandicke = mw;
            //            toothFeed.Text = Formel.Zahnvorschub.ToString();
            //            totalFeed.Text = Formel.Vorschub.ToString();
            //            medChipThickness.Text = Formel.Mittenspandicke.ToString();
            //            zeitspan.Text = Formel.Zeitspanvolumen.ToString();
            //        }
            //        e.Handled = true;
            //    }
            //};

            medChipThickness.FocusChange += delegate 
            {
                double miw;
                count = 0;
                button.Text = "Reset";
                    if (medChipThickness.Text.Contains("."))
                    {
                        medChipThickness.Text = medChipThickness.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                    }
                    // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
                    if (double.TryParse(medChipThickness.Text, out miw))
                    {
                        Formel.Mittenspandicke = miw;
                        toothFeed.Text = Formel.Zahnvorschub.ToString();
                        totalFeed.Text = Formel.Vorschub.ToString();
                        maxChipThickness.Text = Formel.Maximalspandicke.ToString();
                        zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                        
                    }
            };

            //medChipThickness.KeyPress += (object sender, View.KeyEventArgs e) =>
            //{
            //    double miw;
            //    e.Handled = false;
            //    count = 0;
            //    button.Text = "Reset";
            //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        if (medChipThickness.Text.Contains("."))
            //        {
            //            medChipThickness.Text = medChipThickness.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
            //        }
            //        // Toast.MakeText(this, eT_splSpd.Text, ToastLength.Short).Show();
            //        if (double.TryParse(medChipThickness.Text, out miw))
            //        {
            //            Formel.Mittenspandicke = miw;
            //            toothFeed.Text = Formel.Zahnvorschub.ToString();
            //            totalFeed.Text = Formel.Vorschub.ToString();
            //            maxChipThickness.Text = Formel.Maximalspandicke.ToString();
            //            zeitspan.Text = Formel.Zeitspanvolumen.ToString();

            //        }
            //        e.Handled = true;
            //    }
            //};

            btn2.Click += delegate 
            {
                double Vcup;
                if (double.TryParse(forcedVc .Text, out Vcup ))
                {
                    btn2.Activated = false;
                    btn2.Enabled = false;
                    Formel.Schnittgeschwindigkeit = Formel.Schnittgeschwindigkeit * Vcup;
                    zeitspan.Text = Formel.Zeitspanvolumen.ToString();
                    splSpd.Text = Formel.Spindeldrehzahl.ToString();
                    cutSpd.Text = Formel.Schnittgeschwindigkeit.ToString();
                    totalFeed.Text = Formel.Vorschub.ToString();
                }
            };

            button.Click += delegate
            {
                count++;
                if (count < 2)
                {
                    Toast.MakeText(this, "Nochmal drücken zum Beenden", ToastLength.Long).Show();
                    //button.SetBackgroundColor(Android.Graphics.Color.Cyan);
                   

                    button.Text = "Beenden";
                    toolDm.Text = "";
                    cutSpd.Text = "";
                    splSpd.Text = "";
                    toothFeed.Text = "";
                    totalFeed.Text = "";
                    cutWidth.Text = "";
                    toothNbr.Text = "";
                    cutDepth.Text = "";
                    zeitspan.Text = "";
                    maxChipThickness.Text = "";
                    medChipThickness.Text = "";
                    forcedVc.Text = "";
                    drall.Text = "";
                    Formel.Werkzeugdurchmesser = 0;
                    Formel.Zahnvorschub = 0;
                    Formel.Zustelltiefe = 0;
                    Formel.Vorschub = 0;
                    Formel.Eingriffsbreite = 0;
                    Formel.Schnittgeschwindigkeit = 0;
                    Formel.Anzahlschneiden = 0;
                    Formel.Drallwinkel = 0;
                    btn2.Enabled = true;
               }
               
                if (count >= 2)
                {
                    new AlertDialog.Builder(this)
                    .SetMessage("wird beendet")
                    .Show();

                    this.Finish();
                };

                
            };
        }
    }
}


