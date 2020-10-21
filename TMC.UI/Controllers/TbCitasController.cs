﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TMC.BLL.Interfaces;
using TMC.BLL.Metodos;
using TMC.DATA;

namespace TMC.UI.Controllers
{
    public class TbCitasController : Controller
    {
        ICitasBLL cCitas;
        //Creacion de los metodos para las tablas de las FK
        IUsuariosBLL cUsuarios;
        IProgresosBLL cProgresos;
        public TbCitasController()
        {
            cCitas = new MCitasBLL();
            //Construccion de los metodos para las tablas de las FK
            cUsuarios = new MUsuariosBLL();
            cProgresos = new MProgresosBLL();
        }

        //Metodo de carga de los dropdown
        private void CargarListas()
        {
            //cargado en el View
            List<TbUsuarios> usuarios = cUsuarios.Mostrar();
            if (usuarios == null)
            {
                usuarios[0].IDUsuario = 0;
                usuarios[0].correo = "No hay usuarios disponibles";
            }
            ViewBag.ddlCatalogos = new SelectList(usuarios, "IDUsuario", "correo");

            List<TbProgresos> progresos = cProgresos.Mostrar();
            if (progresos == null)
            {
                progresos[0].IDProgreso = 0;
                progresos[0].progreso = "No hay progresos disponibles";
            }
            ViewBag.ddlFotos = new SelectList(progresos, "IDProgreso", "progreso");
        }

        [HttpGet]
        public ActionResult Search()
        {
            var list = cCitas.Mostrar();
            if (list == null) { return RedirectToAction("Create"); };
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CargarListas();
            return View();
        }




        [HttpPost]
        public ActionResult Create(TbCitas citas)
        {
            try
            {
                if (citas.IDUsuario == 0)
                {
                    ModelState.AddModelError(string.Empty, "Debe ingresar un usuario primero");
                    CargarListas();
                    return View();
                }

                if (citas.IDProgreso == 0)
                {
                    ModelState.AddModelError(string.Empty, "Debe ingresar un progreso primero");
                    CargarListas();
                    return View();
                }

                cCitas.Insertar(citas);
                ModelState.AddModelError(string.Empty, "Cita Agregada");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            CargarListas();
            return View();
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var data = cCitas.Buscar(id);
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = cCitas.Buscar(id);
            CargarListas();
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(TbCitas citas)
        {
            if (citas.IDUsuario == 0)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar un usuario primero");
                CargarListas();
                return View();
            }

            if (citas.IDProgreso == 0)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar un progreso primero");
                CargarListas();
                return View();
            }
            cCitas.Actualizar(citas);
            return RedirectToAction("Search");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            /*devuelve un int 0 = Cancelada
             * 1 = No se puede cancelar por el progreso avanzado*/
            cCitas.Cancelar(id);
            return RedirectToAction("Search");
        }
    }
}