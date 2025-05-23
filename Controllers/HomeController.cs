﻿using APIRPA.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
//
using System.Data.Entity;
using System.Web.Http.Results;
using System.Threading.Tasks;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;
using System.Web.Security;
using OpenCvSharp;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;

using Microsoft.AspNetCore.Identity;
using System.Net;
using APIRPA.bl;
using System.IO;
using WebGrease.Activities;
using System.Web.UI.WebControls;

namespace APIRPA.Controllers
{
    public class HomeController : Controller
    {

        // ! GET Home/Index
        // ! CARGA LA PANTALLA PRINCIPAL DE ACCESO
        public ActionResult Index()
        {
            ViewBag.Title = "BIENVENIDO";
            Logger.Instance.Log("INICIO APLICACIÓN");
            Logger.Instance.Log("Carga INDEX");          
            return View();
        } // x GET Home/Index

        // ! GET Home/FormEmail
        public ActionResult FormEmail()
        {
            ViewBag.Title = "Cambio de Email personal";
            return View();
        }

        // ! POST Home/FormEmail
        [HttpPost]
        public ActionResult FormEmail(FormularioEmailModel formularioModel)
        {         
            
            if(ModelState.IsValid)
            {
                FormularioEmailModel obj = new FormularioEmailModel
                {
                    nombreCompletoSolicitante = formularioModel.nombreCompletoSolicitante,
                    nDniSolicitante = formularioModel.nDniSolicitante,
                    emailUnedSolicitante = formularioModel.emailUnedSolicitante,
                    passSolicitante = formularioModel.passSolicitante,
                    emailPersSolicitante = formularioModel.emailPersSolicitante,
                    telfPersSolicitante = formularioModel.telfPersSolicitante,
                };

                if (formularioModel.bytesReversoDni == null)
                {
                    return EmailCambiadoView(formularioModel);
                }
                else
                {
                    var file = formularioModel.bytesReversoDni;
                    byte[] fileByte = new byte[file.ContentLength];
                    file.InputStream.Read(fileByte, 0, fileByte.Length);
                    string fileB64 = Convert.ToBase64String(fileByte);
                    byte[] fileBytes = Convert.FromBase64String(fileB64);

                    return EmailCambiadoView(formularioModel, fileBytes);
                }                     
            }
            return View(formularioModel);
        }

        public ActionResult EmailCambiadoView(FormularioEmailModel formularioModel)
        {
            if (ModelState.IsValid)
            {
                string okCallProcCambioMailBD = "1";

                MailModelo mailMod = new MailModelo();
                mailMod.nombreCompletoSolicitante = formularioModel.nombreCompletoSolicitante;
                mailMod.dniSolicitante = formularioModel.nDniSolicitante;
                mailMod.emailSolicitante = formularioModel.emailUnedSolicitante;
                mailMod.passSolicitante = formularioModel.passSolicitante;
                mailMod.telfPersSolicitante = formularioModel.telfPersSolicitante;
                mailMod.mailNuevo = formularioModel.emailPersSolicitante;

                ValuesController valuesController = new ValuesController();
                // TRY LOGIN 
                var callCambioPassBD = valuesController.cambioMailBD(mailMod, okCallProcCambioMailBD);
                if (callCambioPassBD.ToString() == "0")
                {
                    ModelState.Clear(); // ? TÉCNICAMENTE BORRA LOS DATOS DEL FORMULARIO
                    return View(mailMod);
                }
                else
                {
                    return Redirect("/FormMail");
                }
            }
            else
            {
                return Redirect("./FormMail");
            }
        } // x GET Home/PassCambiadaView

        public ActionResult EmailCambiadoView(FormularioEmailModel formularioModel, byte[] fileBytes)
        {
            if (ModelState.IsValid)
            {
                string okCallProcCambioMailBD = "1";

                MailModelo mailMod = new MailModelo();
                mailMod.nombreCompletoSolicitante = formularioModel.nombreCompletoSolicitante;
                mailMod.dniSolicitante = formularioModel.nDniSolicitante;
                mailMod.emailSolicitante = formularioModel.emailUnedSolicitante;
                mailMod.passSolicitante = formularioModel.passSolicitante;
                mailMod.telfPersSolicitante = formularioModel.telfPersSolicitante;
                mailMod.mailNuevo = formularioModel.emailPersSolicitante;
                mailMod.bytesDNI = fileBytes;

                ValuesController valuesController = new ValuesController();
                // TRY LOGIN 
                var callCambioPassBD = valuesController.cambioMailBD(mailMod, okCallProcCambioMailBD);
                if (callCambioPassBD.ToString() == "0")
                {
                    ModelState.Clear(); // ? TÉCNICAMENTE BORRA LOS DATOS DEL FORMULARIO
                    return View(mailMod);
                }
                else
                {
                    return Redirect("/FormMail");
                }
            }
            else
            {
                return Redirect("./FormMail");
            }
        } // x GET Home/PassCambiadaView


        // ! GET Home/FormPass
        // ! CARGA EL FORMULARIO CON LOS CAMPOS A RELLENAR POR EL USUARIO
        
        public ActionResult FormPasss(FormularioPassModel formularioModel)
        {
            ViewBag.Title = "Recuperación contraseña UNED";
            Logger.Instance.Log("Carga Formulario Pass");
            // TODO DEBERIAMOS DEVOLVER A LA VISTA LOS DATOS DEL MRZ PARA QUE EL USUARIO LOS VISUALIZASE Y EDITASE
            return View();
        } // x GET Home/Form  

        public ActionResult FormPass()
        {
            return View();
        }
        // ! POST Home/FormPass
        // ! POST DE LOS DATOS DEL FORMULARIO
        [HttpPost]
        public ActionResult FormPass(FormularioPassModel formularioModel)
        {
            
            if (ModelState.IsValid)
            {
                Logger.Instance.Log("Campos formulario validados");
                //Para usar el OCR
                if(formularioModel.bytesReversoDni == null)
                {

                    // ! LLAMAMOS A LA FUNCIÓN QUE NO RECIBE  LOS BYTES DE LA IMAGEN
                     return PassCambiadaView(formularioModel);
                }
                else
                {
                    var file = formularioModel.bytesReversoDni;
                    byte[] fileByte = new byte[file.ContentLength];
                    file.InputStream.Read(fileByte, 0, fileByte.Length);
                    string fileB64 = Convert.ToBase64String(fileByte);
                    byte[] fileBytes = Convert.FromBase64String(fileB64);  
                    // ! LLAMAMOS A LA FUNCION QUE RECIBE TAMBIEN LOS BYTES DE LA IMAGEN
                    return PassCambiadaView(formularioModel, fileBytes);
                }
            }
            else
            {
                //return View(formularioModel);
                formularioModel.errorCallCambioPassBD = "Error en la validación del modelo";
                return View(formularioModel);                
            }
        }

        // ! GET Home/PassCambiadaView
        // ! SE USA ESTA FUNCION SI SE HA ADJUNTADO IMAGEN DEL DNI
        public ActionResult PassCambiadaView(FormularioPassModel formularioModel, byte[] fileBytes)
        {
            string okCallProcCambioPassBD = "0";
            if (ModelState.IsValid)
            {
                // ! COMO EL MODELO ES VALIDO AUTORIZAMOS EL PROCESO CON UNA VARIABLE
                 okCallProcCambioPassBD = "1";
                
                formularioModel.arrayBytesDni = fileBytes;

                ValuesController valuesController = new ValuesController();
                var callCambioPassBD = valuesController.cambioPassBD(formularioModel, okCallProcCambioPassBD);         
                
                if (callCambioPassBD.StatusCode == HttpStatusCode.OK)
                {
                    ModelState.Clear(); // ? TÉCNICAMENTE BORRA LOS DATOS DEL FORMULARIO
                    return View("PassCambiadaView");
                }
                else
                {
                    return View(formularioModel);
                }
            }
            else
            {
                return Redirect("./FormPass");
            }

        } // x GET Home/PassCambiadaView

        // ! GET Home/PassCambiadaView
        // ! SE USA ESTA FUNCIÓN SI NO SE HA ADJUNTADO IMAGEN DEL DNI
        public ActionResult PassCambiadaView(FormularioPassModel formularioModel)
        {
            // VOLVEMOS A COMPROBAR QUE EL MODELO DE DATOS RECIBIDO ES VALIDO
            if (ModelState.IsValid)
            {
                Logger.Instance.Log("Validaciones formulario superadas");
                // ! COMO EL MODELO ES VALIDO AUTORIZAMOS EL PROCESO CON UNA VARIABLE
                string okCallProcCambioPassBD = "1";
                Logger.Instance.Log("okCallProcCambioPassBD = 1, cambio de contraseña autorizado");
                               
                ValuesController valuesController = new ValuesController();
                Logger.Instance.Log("Inicio conexion a BBDD");
                var callCambioPassBD = valuesController.cambioPassBD(formularioModel, okCallProcCambioPassBD);
                
                ////////////////////////////////////////////
                if(callCambioPassBD.StatusCode == HttpStatusCode.OK)
                {
                    ModelState.Clear(); // ? TÉCNICAMENTE BORRA LOS DATOS DEL FORMULARIO
                                        //return View(miMod);
                    return View("PassCambiadaView");

                }
                else
                {
                    if (callCambioPassBD.StatusCode == HttpStatusCode.NotFound)
                    {
                        formularioModel.errorCallCambioPassBD = "NOT FOUND // RESPONSE: 1";
                    }
                    else
                    {
                        if(callCambioPassBD.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            formularioModel.errorCallCambioPassBD = "INTERNAL SERVER ERROR // RESPONSE: 2";
                        }
                        else
                        {
                            if (callCambioPassBD.StatusCode == HttpStatusCode.NoContent)
                            {
                                formularioModel.errorCallCambioPassBD = "NO CONTENT // RESPONSE: 4";
                            }
                            else
                            {
                                if (callCambioPassBD.StatusCode == HttpStatusCode.BadRequest)
                                {
                                    // BAD REQUEST
                                    formularioModel.errorCallCambioPassBD = "BAD REQUEST // BBDD RESPONSE = NULL";

                                }
                                else
                                {
                                    if (callCambioPassBD.StatusCode == HttpStatusCode.ExpectationFailed)
                                    {
                                        formularioModel.errorCallCambioPassBD = "EXCEPCION EN EL CODIGO";
                                    }
                                    else
                                    {
                                        if(callCambioPassBD.StatusCode == HttpStatusCode.Forbidden)
                                        {
                                            formularioModel.errorCallCambioPassBD = "callCambioPass = 0 // NO AUTORIZADO EN EL CÓDIGO";
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return View(formularioModel);
                }
                //return View(formularioModel);
            }
            else
            {
                return Redirect("./FormPass");
            }
        } // x GET Home/PassCambiadaView

        // ! GET Home/FormDniPass
        public ActionResult FormDniPass()
        {
            ViewBag.Title = "PRUEBA";
            Logger.Instance.Log("Carga FormDniPass");
            return View();
        }

        // ! POST Home/FormDniPass
        [HttpPost]
        public ActionResult FormDniPass(FormularioDniModel formularioDniModel)
        {
            Logger.Instance.Log("INICIO proceso OCR");
            // ! VARIABLES GLOBALES 
            string ocrFound;
            string mrzFound;
            var mrzFoundToVerify = false;
            if (ModelState.IsValid)
            {
                Logger.Instance.Log("Imagen adjuntada al formulario");
                // ! CONVERSIONES DEL DNI PARA PODER TRATAR SU IMAGEN
                var file = formularioDniModel.bytesReversoDni;
                byte[] fileByte = new byte[file.ContentLength];
                file.InputStream.Read(fileByte, 0, fileByte.Length);
                string fileB64 = Convert.ToBase64String(fileByte);
                byte[] fileBytes = Convert.FromBase64String(fileB64);
                // ! GUARDAMOS LOS BYTES EN MEMORIA
                MemoryStream memoryStream = new MemoryStream(fileBytes);
                Logger.Instance.Log("Imagen guardada en MemoryStream");
                // ! INICIALIZAMOS LA CLASE QUE REALIZA EL RECONOCIMIENTO OPTICO DE CARACTERES (OCR)
                MakeOCR makeOCR = new MakeOCR();
                // ! REALIZAMOS EL OCR Y LO ASIGNAMOS A OCRFOUND
                Logger.Instance.Log("Iniciamos Proceso OCR");
                ocrFound = makeOCR.getOCR(fileBytes, memoryStream);
                Logger.Instance.Log("OCR Realizado: \n ---------- \n" + ocrFound + "\n ---------" );
                // ! INICIALIZAMOS LA CLASE QUE SEPARA EL OCR EN LOS CAMPOS DEL MRZ
                
                mrzDetector mrzDetector = new mrzDetector();
                // ! OBTENEMOS LAS LINEAS MRZ DEL DNI (RECIBE UN STRING CON EL OCR Y DEVUELVE LAS 2 LINEAS NECESARIAS DEL MRZ EN UN STRING SEPARADAS POR '*' )
                mrzFound = mrzDetector.getMrzArray(ocrFound);
                Logger.Instance.Log("MRZ Realizado: \n ---------- \n" + mrzFound + "\n ----------" );
                // ! INICIALIZAMOS EL OBJETO FORMULARIODNIMODEL QUE CONTIENE LA IMAGEN DEL DNI
                FormularioDniModel dniModel = new FormularioDniModel();
                // ! INICIALIZAMOS EL OBJETO FORMULARIOPASSMODEL QUE CONTIENE LOS DATOS DEL USUARIO
                FormularioPassModel formularioPassModel = new FormularioPassModel();
                if (ocrFound.Contains("Ha ocurrido un error"))
                {
                    formularioPassModel.errorOCR = ocrFound;
                }
                // ! SEPARAMOS EN UN ARRAY LAS LINEAS DEL MRZ QUE SABEMOS QUE ESTAN CONCATENADAS CON '*' ENTRE LAS 2
                string[] lineasMrz = mrzFound.Split('*');
                // CONFIRMAMOS QUE AL MENOS LAS LINEAS SON DE LA LONGITUD CORRECTA
                if (lineasMrz[0].Length != 30 || lineasMrz[1].Length != 30)
                {
                    //ERROR SALIDA DE PROGRAMA
                    formularioPassModel.Linea1Mrz = "ERROR, NO SE HA PODIDO REALIZAR EL ORC, POR FAVOR RELLENE LOS CAMPOS A MANO";
                    formularioPassModel.Linea2Mrz = "ERROR, NO SE HA PODIDO REALIZAR EL ORC, POR FAVOR RELLENE LOS CAMPOS A MANO";
                    Logger.Instance.Log("Error al obtener el MRZ, OCR incorrecto. Los datos serán introducidos a mano");
                }
                else
                {
                    // ! REASIGNAMOS AL MODELO LAS LINEAS DE MRZ
                    formularioPassModel.Linea1Mrz = lineasMrz[0];
                    formularioPassModel.Linea2Mrz = lineasMrz[1];
                    Logger.Instance.Log("MRZ obtenido");
                }

                
                // DEVOLVEMOS VACIOS EL RESTO DE CAMPOS
                formularioPassModel.nombreCompletoSolicitante = "";
                formularioPassModel.nDniSolicitante = "";
                formularioPassModel.emailUnedSolicitante = "";
                formularioPassModel.emailPersSolicitante = "";
                
                // DEVOLVEMOS LA VISTA FormPass con los datos obtenidos del MRZ
                return View("FormPass", formularioPassModel);
            }
            else
            {
                return View(formularioDniModel);
            }
        }    
    }
}
