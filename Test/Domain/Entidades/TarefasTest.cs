using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskMasterApi.Entities;
using System;

namespace Test.Domain.Entidades
{
    [TestClass]
    public class TarefaTest
    {
        [TestMethod]
        public void Id_DeveSerSetadoCorretamente()
        {
            var tarefa = new Tarefa();
            tarefa.Id = 1;
            Assert.AreEqual(1, tarefa.Id);
        }

        [TestMethod]
        public void Titulo_DeveSerSetadoCorretamente()
        {
            var tarefa = new Tarefa();
            tarefa.Titulo = "Aprender C#";
            Assert.AreEqual("Aprender C#", tarefa.Titulo);
        }

        [TestMethod]
        public void Descricao_DeveSerSetadoCorretamente()
        {
            var tarefa = new Tarefa();
            tarefa.Descricao = "Estudar MSTest";
            Assert.AreEqual("Estudar MSTest", tarefa.Descricao);
        }

        [TestMethod]
        public void Data_DeveSerSetadoCorretamente()
        {
            var tarefa = new Tarefa();
            var data = DateTime.Now;
            tarefa.Data = data;
            Assert.AreEqual(data, tarefa.Data);
        }

        [TestMethod]
        public void Status_DeveSerSetadoCorretamente()
        {
            var tarefa = new Tarefa();
            tarefa.Status = EnumStatusTarefa.Pendente; // exemplo
            Assert.AreEqual(EnumStatusTarefa.Pendente, tarefa.Status);
        }
    }
}
