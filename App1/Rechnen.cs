using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    class Rechnen
{

        private double _Werkzeugdurchmesser;
        private double _Schnittgeschwindigkeit;
        private double _Spindeldrehzahl;
        private double _Zahnvorschub;
        private double _Vorschub;
        private double _Eingriffsbreite;
        private double _Zustelltiefe;
        private double _Zeitspanvolumen;
        private double _VcFaktor;
        private double _AeFaktor;
        private double _Maximalspandicke;
        private double _Mittenspandicke;
        private double _Eingriffswinkel;
        private int _Anzahlschneiden;
        private double _Drallwinkel;
        private double _Einstellwinkel = 90;
        private double _Schneideneingriffslaengeradial;
        private double _Schneideneingriffslaengereal;

        private double _Zahnabstandaxial;
        private double _Zahnabstandradial;

        
        private double _Gesamtschneidenlaenge;
        private double _Fraeserumfang;
        private double _Schnittbogenlaenge;
        private double _ASIE;
        

        private void RechneEingriff()
        {
            double axfactor;
            axfactor = _Zustelltiefe / _Zahnabstandaxial;
            


            if (_Schneideneingriffslaengeradial <= _Schnittbogenlaenge )
            {
                _Schneideneingriffslaengereal = _Zustelltiefe / Math.Cos(_Drallwinkel * Math.PI / 180);

            }
            else if (_Schneideneingriffslaengeradial > _Schnittbogenlaenge)
            {
                _Schneideneingriffslaengereal = _Schnittbogenlaenge / Math.Cos(_Einstellwinkel * Math.PI / 180);
                
            }
            // es gilt zu rechnen: Gesamtschneidenlänge im Eingriff auf Umfang pro Umdrehung, weiter Verhältnis Zahnabstandradial zu Eingriffsbogenlänge?!
            // welches schon reichen könnte. Irgendwie muss aber die Zähnezahl noch verwurstet werden? 
            // bis hier stimmt es
            _Gesamtschneidenlaenge = _Schneideneingriffslaengereal * axfactor * _Anzahlschneiden;

            _ASIE = _Gesamtschneidenlaenge / _Fraeserumfang;
        }

        public double Gesamtschneidenlaenge
        {
            get
            {
                return _Gesamtschneidenlaenge;
            }
        }


        public double Zahnabstandaxial
        {
            get
            {
                return _Zahnabstandaxial;
            }
        }


        public double Zahnabstandradial
        {
            get
            {
                return _Zahnabstandradial;
            }
        }

        public double Schneideneingriffslaengeradial
        { get
            {
                return _Schneideneingriffslaengeradial;
            }
        }

        public double Schneideneingriffslaengereal
        {
            get
            {
                return _Schneideneingriffslaengereal;
            }
        }

        public double Schnittbogenlaenge
        {
            get
            {
                return _Schnittbogenlaenge;
            }
            set
            {
                _Schnittbogenlaenge = value;
            }
        }


        public double ASIE
        {
            get
            {
                return _ASIE;
            }
        }

        public double RechneMittenspan(double zv, double eb, double wd)
        {

            return zv * System.Math.Sqrt(eb / wd);
        }

 
        public double Einstellwinkel
        {
            get
            {
                return _Einstellwinkel;
            }
            set
            {
                _Einstellwinkel = value;
            }
        }

        public double Drallwinkel
        {
            get
            {
                return _Drallwinkel;
            }
            set
            {
                _Drallwinkel = value;
                _Einstellwinkel = 90 - _Drallwinkel;
                if (_Werkzeugdurchmesser > 0 && _Anzahlschneiden > 0)
                {
                    _Zahnabstandradial = (_Werkzeugdurchmesser * Math.PI) / _Anzahlschneiden;
                }
                    if (_Anzahlschneiden != 0) _Zahnabstandaxial = _Zahnabstandradial * Math.Tan((90 - _Drallwinkel) * (Math.PI / 180));
                if (_Zustelltiefe > 0)
                {
                    _Schneideneingriffslaengeradial = _Zustelltiefe * Math.Tan(_Drallwinkel * (Math.PI / 180));
                    RechneEingriff();
                }
                
            }
        }

        public double Maximalspandicke
        {
            get
            {
                return _Maximalspandicke;
            }
            set
            {
                _Maximalspandicke = value;
                _Zahnvorschub = _Maximalspandicke / Math.Sin(_Eingriffswinkel * Math.PI / 180);
                _Vorschub = _Zahnvorschub * _Spindeldrehzahl * _Anzahlschneiden;
                _Zeitspanvolumen = _Zustelltiefe * _Eingriffsbreite * _Vorschub / 1000;
                _Mittenspandicke = _Zahnvorschub * Math.Sqrt(_Eingriffsbreite / _Werkzeugdurchmesser);
            }
        }

        public double Mittenspandicke
        {
            get
            {
                return _Mittenspandicke;
            }
            set
            {
                _Mittenspandicke = value;
                _Zahnvorschub = _Mittenspandicke / Math.Sqrt(_Eingriffsbreite / _Werkzeugdurchmesser);
                _Vorschub = _Zahnvorschub * _Spindeldrehzahl * _Anzahlschneiden;
                _Zeitspanvolumen = _Zustelltiefe * _Eingriffsbreite * _Vorschub / 1000;
                if (_Eingriffswinkel > 0 && _Eingriffswinkel <= 90)
                {
                    _Maximalspandicke = _Zahnvorschub * System.Math.Sin(_Eingriffswinkel * System.Math.PI / 180);
                }
                else if (_Eingriffswinkel > 90)
                {
                    _Maximalspandicke = _Zahnvorschub;
                }
            }
        }



        public double Zeitspanvolumen
        {
            get
            {
                return _Zeitspanvolumen;
            }
        }

        public int Anzahlschneiden
        {
            get
            {
                return _Anzahlschneiden;
            }
            set
            {
                _Anzahlschneiden = value;
                if (_Werkzeugdurchmesser >0 && _Anzahlschneiden > 0)
                {
                    _Zahnabstandradial = (_Werkzeugdurchmesser * Math.PI) / _Anzahlschneiden;
                    _Zahnabstandaxial = _Zahnabstandradial * Math.Tan((90 - _Drallwinkel) * (Math.PI / 180));
                }
                if (_Spindeldrehzahl != 0)
                {
                    _Vorschub = _Spindeldrehzahl * _Anzahlschneiden * _Zahnvorschub;
                    _Zeitspanvolumen = _Zustelltiefe * _Eingriffsbreite * _Vorschub / 1000;
                }
            }
        }


        public double Zustelltiefe
        {
            get
            {
                return _Zustelltiefe;
            }
            set
            {
                _Zustelltiefe = value;
                _Zeitspanvolumen= _Zustelltiefe * _Eingriffsbreite * _Vorschub / 1000;
                _Schneideneingriffslaengeradial = _Zustelltiefe * Math.Tan(_Drallwinkel * (Math.PI / 180));
                RechneEingriff();
                
            }
        }


        public double Vorschub
        {
            get
            {
                return _Vorschub;
            }
            set
            {
                _Vorschub = value;
                if (_Spindeldrehzahl!=0 && _Anzahlschneiden != 0)
                {
                    _Zahnvorschub = _Vorschub / (_Spindeldrehzahl * _Anzahlschneiden);
                    _Zeitspanvolumen = _Zustelltiefe * _Eingriffsbreite * _Vorschub / 1000;
                    if (_Werkzeugdurchmesser != 0) _Mittenspandicke = _Zahnvorschub * System.Math.Sqrt(_Eingriffsbreite / _Werkzeugdurchmesser);
                    if (_Eingriffswinkel > 0 && _Eingriffswinkel <= 90)
                    {
                        _Maximalspandicke = _Zahnvorschub * System.Math.Sin(_Eingriffswinkel * System.Math.PI / 180);
                    }
                    else if (_Eingriffswinkel > 90)
                    {
                        _Maximalspandicke = _Zahnvorschub;
                    }                   
                }
            }
        }





        public double Zahnvorschub
        {
            get
            {
                return _Zahnvorschub;
            }
            set
            {
                _Zahnvorschub = value;
                if (_Spindeldrehzahl != 0 && _Anzahlschneiden !=0)
                {
                    _Vorschub=_Spindeldrehzahl * _Anzahlschneiden* _Zahnvorschub;
                    _Zeitspanvolumen = _Zustelltiefe * _Eingriffsbreite * _Vorschub / 1000;
                    //_Mittenspandicke = RechneMittenspan(_Zahnvorschub ,_Eingriffsbreite, _Werkzeugdurchmesser);
                    _Mittenspandicke = _Zahnvorschub * System.Math.Sqrt(_Eingriffsbreite / _Werkzeugdurchmesser);
                    if (_Eingriffswinkel > 0 && _Eingriffswinkel <= 90)
                    {
                        _Maximalspandicke = _Zahnvorschub * System.Math.Sin(_Eingriffswinkel * System.Math.PI / 180);
                    }
                    else if (_Eingriffswinkel > 90)
                    {
                        _Maximalspandicke = _Zahnvorschub;
                    }
                }
            }
        }

        public double Spindeldrehzahl
        {
            get
            {
                return _Spindeldrehzahl;
            }
            set
            {
                _Spindeldrehzahl = value;
                _Schnittgeschwindigkeit = _Spindeldrehzahl * _Werkzeugdurchmesser * System.Math.PI / 1000;
                _Vorschub = _Zahnvorschub * _Spindeldrehzahl * _Anzahlschneiden;
                _Zeitspanvolumen = _Zustelltiefe * _Eingriffsbreite * _Vorschub / 1000;
            }
        }
        
        public double Eingriffsbreite
        {
            get
            {
                return _Eingriffsbreite;
            }
            set
            {
                _Eingriffsbreite = value;
                if (_Werkzeugdurchmesser != 0 && _Eingriffsbreite != 0)
                {
                    _AeFaktor = _Eingriffsbreite / _Werkzeugdurchmesser;
                    _Eingriffswinkel = System.Math.Acos((0.5 * _Werkzeugdurchmesser - _Eingriffsbreite) / (0.5 * _Werkzeugdurchmesser)) * 180 / System.Math.PI;
                    _Schnittbogenlaenge = _Werkzeugdurchmesser * Math.PI * (_Eingriffswinkel / 360);
                    if (_Zustelltiefe > 0)
                    {
                        _Schneideneingriffslaengeradial = _Zustelltiefe * Math.Tan(_Drallwinkel * (Math.PI / 180));
                        RechneEingriff();
                    }

                    if (_Zahnvorschub != 0)
                    {
                        _Zeitspanvolumen = _Zustelltiefe * _Eingriffsbreite * _Vorschub / 1000;
                        _Mittenspandicke = _Zahnvorschub * System.Math.Sqrt(_Eingriffsbreite / _Werkzeugdurchmesser);
                        if (_Eingriffswinkel > 0 && _Eingriffswinkel <= 90)
                        {
                            _Maximalspandicke = _Zahnvorschub * System.Math.Sin(_Eingriffswinkel * System.Math.PI / 180);
                        }
                        else if (_Eingriffswinkel > 90)
                        {
                            _Maximalspandicke = _Zahnvorschub;
                        }
                    }
                }
            }
        }

        public double Eingriffswinkel
        {
            get
            {
                return _Eingriffswinkel;
            }
        }

        public double Werkzeugdurchmesser
        {
            get
            {
                return _Werkzeugdurchmesser;
            }
            set
            {
                _Werkzeugdurchmesser = value;
                if (_Werkzeugdurchmesser > 0 && _Anzahlschneiden > 0)
                {
                    _Zahnabstandradial = (_Werkzeugdurchmesser * Math.PI) / _Anzahlschneiden;
                    _Zahnabstandaxial = _Zahnabstandradial * Math.Tan((90 - _Drallwinkel) * (Math.PI / 180));
                }
                _Fraeserumfang = _Werkzeugdurchmesser * Math.PI;
                if (_Schnittgeschwindigkeit != 0 && _Werkzeugdurchmesser != 0)
                {
                    _Spindeldrehzahl = _Schnittgeschwindigkeit * 1000 / System.Math.PI / _Werkzeugdurchmesser;
                }
                if (_Werkzeugdurchmesser != 0 && _Eingriffsbreite != 0)
                {
                    _AeFaktor = _Eingriffsbreite / _Werkzeugdurchmesser;
                    _Eingriffswinkel = System.Math.Acos((0.5 * _Werkzeugdurchmesser - _Eingriffsbreite) / (0.5 * _Werkzeugdurchmesser)) * 180 / System.Math.PI;
                }
            }
        }

        public double Schnittgeschwindigkeit
        {
            get
            {
                return _Schnittgeschwindigkeit;
            }
            set
            {
                _Schnittgeschwindigkeit = value;
                if (_Werkzeugdurchmesser != 0 && _Schnittgeschwindigkeit != 0)
                {
                    _Spindeldrehzahl = _Schnittgeschwindigkeit * 1000 / System.Math.PI / _Werkzeugdurchmesser;
                    if (_Zahnvorschub != 0 && _Anzahlschneiden != 0)
                    {
                        _Vorschub = _Zahnvorschub * _Spindeldrehzahl * _Anzahlschneiden;
                        if (_Eingriffsbreite != 0)
                        {
                            _Zeitspanvolumen = _Zustelltiefe * _Eingriffsbreite * _Vorschub / 1000;
                            _Mittenspandicke = _Zahnvorschub * System.Math.Sqrt(_Eingriffsbreite / _Werkzeugdurchmesser);
                            if (_Eingriffswinkel > 0 && _Eingriffswinkel <= 90)
                            {
                                _Maximalspandicke = _Zahnvorschub * System.Math.Sin(_Eingriffswinkel * System.Math.PI / 180);
                            }
                            else if (_Eingriffswinkel > 90)
                            {
                                _Maximalspandicke = _Zahnvorschub;
                            }
                        }
                    }
                }
            }
        }




        public double AeFaktor
        {
            get
            {
                return _AeFaktor;
            }
            set
            {
                _AeFaktor = value;
                RechneVcFaktor();
            }
        }

       public void RechneVcFaktor()
        {
            if ((_AeFaktor > 0) && (_AeFaktor <= 0.02)) _VcFaktor = 1.8;
            else if ((_AeFaktor > 0.02) && (_AeFaktor <= 0.03)) _VcFaktor = 1.7;
            else if ((_AeFaktor > 0.03) && (_AeFaktor <= 0.05)) _VcFaktor = 1.6;
            else if ((_AeFaktor > 0.05) && (_AeFaktor <= 0.1)) _VcFaktor = 1.5;
            else if ((_AeFaktor > 0.1) && (_AeFaktor <= 0.15)) _VcFaktor = 1.4;
            else if ((_AeFaktor > 0.15) && (_AeFaktor <= 0.2)) _VcFaktor = 1.35;
            else if ((_AeFaktor > 0.2) && (_AeFaktor <= 0.3)) _VcFaktor = 1.3;
            else if ((_AeFaktor > 0.3) && (_AeFaktor <= 0.4)) _VcFaktor = 1.25;
            else if ((_AeFaktor > 0.4) && (_AeFaktor <= 0.5)) _VcFaktor = 1.2;
            else _VcFaktor = 1;


        }

        public double VcFaktor
        {
            get
            {
                RechneVcFaktor();
                return _VcFaktor;
            }
        }
    }
}
    