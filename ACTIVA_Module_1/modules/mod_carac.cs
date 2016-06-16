﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;

namespace ACTIVA_Module_1.modules
{
    class mod_carac
    {
        // Vérifier si on veut ajouter un nouveau tab ou modifier
        public static bool cree;

        public static void New_Caracteristique_Form(string code, string num, bool clone, string parent)
        {
            XmlNode root;
            XmlNode nodeIntitule;
            XmlNode nodeAjout;
            C1.Win.C1Command.C1DockingTabPage CTab = new C1.Win.C1Command.C1DockingTabPage();
            ACTIVA_Module_1.component.carac_panel Cpanel = new ACTIVA_Module_1.component.carac_panel(code, num, clone);
            Cpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            
            //On récupère l'intitulé du code
            root = mod_global.Get_Codes_Obs_DocElement();
            nodeIntitule = root.SelectSingleNode("/codes/code[id='" + code + "']/intitule");
            nodeAjout = root.SelectSingleNode("/codes/code[id='" + code + "']");
            CTab.Text =  code + " | " + nodeIntitule.InnerText;

            // Hériter les caractéristiques des codes liéés
            if (parent != null)
            {
                XmlNode SVF = modules.mod_accueil.SVF.DocumentElement;
                XmlNode codenode = SVF.SelectSingleNode("ouvrage[@nom='" + mod_accueil.OUVRAGE + "']/observations/code[id='" + parent + "']");
                // h1
                if (codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "h1" + "']").InnerText != "")
                Cpanel.EmpCirc1Tb.Text = codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "h1" + "']").Attributes["correspondance"].InnerText + " | " +
                                         codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "h1" + "']").InnerText;
                // h2
                if (codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "h2" + "']").InnerText != "")
                Cpanel.EmpCirc2Tb.Text = codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "h2" + "']").Attributes["correspondance"].InnerText + " | " +
                                         codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "h2" + "']").InnerText;
                // pm1
                if (codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "pm1" + "']").InnerText != "")
                Cpanel.EmpLong1Tb.Text = codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "pm1" + "']").InnerText;
                // pm2
                if (codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "pm2" + "']").InnerText != "")
                Cpanel.EmpLong2Tb.Text = codenode.SelectSingleNode("caracteristiques/caracteristique[@nom='" + "pm2" + "']").InnerText;
            }

            if (nodeAjout.Attributes["ajoute"] != null)
                if (nodeAjout.Attributes["ajoute"].InnerText == "true")
                {
                    CTab.TabForeColorSelected = Color.RoyalBlue;
                    CTab.ForeColor = Color.RoyalBlue;
                    Cpanel.SetColor(Color.RoyalBlue);
                }
                else Cpanel.SetColor(Color.Black);
            CTab.Controls.Add(Cpanel);

            /*for (int i = 0; i < mod_global.MF.CaracDockingTab.TabPages.Count; i++){
                if (mod_global.MF.CaracDockingTab.TabPages[i] == CTab)
                    return;
            }*/
            mod_global.MF.CaracDockingTab.TabPages.Add(CTab);

        }

        public static void Add_Existing_Code_Tab(string code, string num, bool clone)
        {
            cree = false;
            //On supprime les anciennes pages de codes
            if (clone == false)
                mod_global.MF.CaracDockingTab.TabPages.Clear();

            //Création de la page du code sélectionné dans ObservationGrid
            New_Caracteristique_Form(code, num, clone, null);

            mod_global.MF.CaracDockingTab.SelectedTab = mod_global.MF.CaracDockingTab.TabPages[0];
            mod_global.MF.MainDockingTab.SelectedTab = mod_global.MF.RenseignementTab;
        }


        public static void Add_New_Code_Tabs(string code)
        {
            cree = true;
            //On supprime les anciennes pages de codes
            mod_global.MF.CaracDockingTab.TabPages.Clear();

            //Création de la page du code principal (si num=String.Empty, la page créée est vide)
            New_Caracteristique_Form(code, String.Empty, false, null);



            mod_global.MF.CaracDockingTab.SelectedTab = mod_global.MF.CaracDockingTab.TabPages[0];
            mod_global.MF.MainDockingTab.SelectedTab = mod_global.MF.RenseignementTab;
        }
    }



}
