using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightInfocon.GoldenAccess.General;


namespace FCarnauba_Animais.DataAccess
{

    public class FCarnaubaFacade
    {

        public List<ResultadoBuscaAnimal> ConsultaAnimal(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<ResultadoBuscaAnimal> animais = dataAccess.ConsultaAnimal(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return animais;
        }

        public Animal[] ConsultaDdlAnimal(CriterioPesquisaAnimal criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var animais = dataAccess.ConsultaDdlAnimal(criterio);
            dataAccess.CloseConnection();
            return animais;
        }

        public List<RRankingPeso> ConsultaRankingPeso(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RRankingPeso> rankingsPeso = dataAccess.ConsultaRankingPeso(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return rankingsPeso;
        }

        public List<RRankingFilhos> ConsultaRankingFilhos(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RRankingFilhos> rankingsFilhos = dataAccess.ConsultaRankingFilhos(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return rankingsFilhos;
        }

        public long AdicionaAnimal(Animal animal)
        {

            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            long idAnimal = dataAccess.AdicionaAnimal(animal);
            dataAccess.CloseConnection();
            return idAnimal;
        }

        public void SalvaAnimal(Animal animal)
        {

            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaAnimal(animal);
            dataAccess.CloseConnection();
        }


        public void RemoveAnimal(string strId)
        {

            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveAnimal(strId);
            dataAccess.CloseConnection();
        }

        public Animal GetAnimalById(string strId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Animal animal = dataAccess.GetAnimalById(strId);
            dataAccess.CloseConnection();
            return animal;
        }

        public Animal GetAnimalByIdCompleto(string strId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Animal animal = dataAccess.GetAnimalByIdCompleto(strId);
            dataAccess.CloseConnection();
            return animal;
        }

        public List<Historico> GetHistorico(int animalId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetHistorico(animalId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public Historico GetHistoricoByIndex(int animalId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Historico h = dataAccess.GetHistoricoById(animalId, index);
            dataAccess.CloseConnection();
            return h;
        }

        public void AdicionaHistorico(string id, Historico historico)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaHistorico(id, historico);
            dataAccess.CloseConnection();
        }

        public void SalvaHistorico(string animalId, Historico historico, int historicoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaHistorico(animalId, historico, historicoId);
            dataAccess.CloseConnection();
        }

        public void RemoveHistorico(int animalId, int historicoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveHistorico(animalId, historicoId);
            dataAccess.CloseConnection();
        }

        public User AutenticaUsuario(string userName, string password)
        {
            GoldenAccess goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
            User user = goldenAccess.Authenticate(userName, password);
            return user;
        }

        public RMachoFemea GetMachoFemea(string strId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            RMachoFemea machoFemea = dataAccess.GetMachoFemea(strId);
            dataAccess.CloseConnection();
            return machoFemea;
        }

        public List<Animal> GetAnimais()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetAnimais();
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetAnimaisComVazio()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetAnimaisComVazio();
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetAnimais(string raca)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetAnimais(raca);
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetAnimais(string raca, string nomeOuRgd)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetAnimais(raca, nomeOuRgd);
            dataAccess.CloseConnection();
            return animais;
        }

        public ItemDescFinanceiro[] GetDescricoesFinanceiro()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            ItemDescFinanceiro[] itens = dataAccess.GetDescricoesFinanceiro();
            dataAccess.CloseConnection();
            return itens;
        }

        public int GetIdGrupoDescricaoFinanceiro(string descricao)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            int idGrupo = dataAccess.GetIdGrupoDescricaoFinanceiro(descricao);
            dataAccess.CloseConnection();
            return idGrupo;
        }

        public string GetDescricaoIdGrupo(int idGrupo)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            string descricao = dataAccess.GetDescricaoIdGrupo(idGrupo);
            dataAccess.CloseConnection();
            return descricao;
        }

        public int GetEntradaDesembolsoIdGrupo(int idGrupo)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            int entradaDesembolso = dataAccess.GetEntradaDesembolsoIdGrupo(idGrupo);
            dataAccess.CloseConnection();
            return entradaDesembolso;
        }

        public List<Animal> GetPais()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetPais();
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetPais(string raca)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetPais(raca);
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetPais(string raca, string nomeOuRgd)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetPais(raca, nomeOuRgd);
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetMaes()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetMaes();
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetMaes(string raca)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetMaes(raca);
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetMaes(string raca, string nomeOuRgd)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetMaes(raca, nomeOuRgd);
            dataAccess.CloseConnection();
            return animais;
        }

        public List<RRankingIppIepEr> ConsultaRankingIpp(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RRankingIppIepEr> rankingsIpp = dataAccess.ConsultaRankingIpp(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return rankingsIpp;
        }

        public List<RRankingIppIepEr> ConsultaRankingIep(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RRankingIppIepEr> rankingsIep = dataAccess.ConsultaRankingIep(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return rankingsIep;
        }

        public List<RRankingIppIepEr> ConsultaRankingKgIep(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RRankingIppIepEr> rankingsKgIep = dataAccess.ConsultaRankingKgIep(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return rankingsKgIep;
        }

        public List<RRankingIppIepEr> ConsultaRankingAcumuladaTotal(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RRankingIppIepEr> rankingsAcumuladaTotal = dataAccess.ConsultaRankingAcumuladaTotal(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return rankingsAcumuladaTotal;
        }

        public List<RRankingIppIepEr> ConsultaRankingEr(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RRankingIppIepEr> rankingsEr = dataAccess.ConsultaRankingEr(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return rankingsEr;
        }

        public List<RAnimaisAno> ConsultaAnimaisAno(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RAnimaisAno> animaisAno = dataAccess.ConsultaAnimaisAno(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return animaisAno;
        }

        public List<RAnimaisLactacaoAno> ConsultaAnimaisLactacaoAno(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RAnimaisLactacaoAno> animaisLactacaoAno = dataAccess.ConsultaAnimaisLactacaoAno(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return animaisLactacaoAno;
        }

        public List<Noh> GetListPais(string strId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Noh> pais = dataAccess.GetListPais(strId);
            dataAccess.CloseConnection();
            return pais;
        }

        public List<Noh> GetListMaes(string strId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Noh> maes = dataAccess.GetListMaes(strId);
            dataAccess.CloseConnection();
            return maes;
        }

        public string GetIdPai(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            string pai = dataAccess.GetIdPai(id);
            dataAccess.CloseConnection();
            return pai;
        }

        public string GetIdMae(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            string mae = dataAccess.GetIdMae(id);
            dataAccess.CloseConnection();
            return mae;
        }

        public List<RPluviometria> GetPluviometrias(int ano, string propriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RPluviometria> pluviometrias = dataAccess.GetPluviometrias(ano, propriedade);
            dataAccess.CloseConnection();
            return pluviometrias;
        }

        public List<RPluviometria> GetPluviometriasDet(int ano, string propriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RPluviometria> pluviometrias = dataAccess.GetPluviometriasDet(ano, propriedade);
            dataAccess.CloseConnection();
            return pluviometrias;
        }

        public List<RPluviometria> GetPluviometriasDet(DateTime dataInicial, DateTime dataFinal, string propriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RPluviometria> pluviometrias = dataAccess.GetPluviometriasDet(dataInicial, dataFinal, propriedade);
            dataAccess.CloseConnection();
            return pluviometrias;
        }

        public List<RPluviometria> GetTodasPluviometrias(DateTime dataInicial, DateTime dataFinal)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RPluviometria> pluviometrias = dataAccess.GetTodasPluviometrias(dataInicial, dataFinal);
            dataAccess.CloseConnection();
            return pluviometrias;
        }

        public List<RPluviometria> GetPluviometrias(DateTime dataInicial, DateTime dataFinal, string propriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RPluviometria> pluviometrias = dataAccess.GetPluviometrias(dataInicial, dataFinal, propriedade);
            dataAccess.CloseConnection();
            return pluviometrias;
        }

        public List<RPluviometria> GetPluviometrias(string propriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RPluviometria> pluviometrias = dataAccess.GetPluviometrias(propriedade);
            dataAccess.CloseConnection();
            return pluviometrias;
        }

        public List<RPeso> GetPesos(ParametrosDeBuscaEmLotesPonderais parametros)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RPeso> pesos = dataAccess.GetPesos(parametros);
            dataAccess.CloseConnection();
            return pesos;
        }

        public List<string> GetSimplesPropriedades()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<string> propriedades = dataAccess.GetSimplesPropriedades();
            dataAccess.CloseConnection();
            return propriedades;
        }

        public RLoteControleLeiteiro GetLoteControleLeiteiroData(DateTime dataControleLeiteiro)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            RLoteControleLeiteiro controleLeiteiro = dataAccess.GetLoteControleLeiteiroData(dataControleLeiteiro);
            dataAccess.CloseConnection();
            return controleLeiteiro;
        }

        public List<RProducaoLeite> GetControleProducaoLeiteiroData(long idLote)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RProducaoLeite> producoes = dataAccess.GetControleProducaoLeiteiroData(idLote);
            dataAccess.CloseConnection();
            return producoes;
        }

        public List<RProducaoLeite> GetControleProducaoLeiteiroTotal(DateTime dataInicio, DateTime dataFim, string idPropriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RProducaoLeite> producoes = dataAccess.GetControleProducaoLeiteiroTotal(dataInicio, dataFim, idPropriedade);
            dataAccess.CloseConnection();
            return producoes;
        }

        public List<Lote> GetLotes()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Lote> lotes = dataAccess.GetLotes();
            dataAccess.CloseConnection();
            return lotes;
        }

        public List<Lote> GetLotesParaPesagens()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Lote> lotes = dataAccess.GetLotesParaPesagens();
            dataAccess.CloseConnection();
            return lotes;
        }

        public List<ResultadoBuscaLote> ConsultaLote(ParametrosDeBuscaEmLotes parametrosBuscaEmLotes)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<ResultadoBuscaLote> lotes = dataAccess.ConsultaLote(parametrosBuscaEmLotes);
            dataAccess.CloseConnection();
            return lotes;
        }

        public Lote GetLoteById(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Lote lote = dataAccess.GetLoteById(id);
            dataAccess.CloseConnection();
            return lote;
        }

        public ItemFinanceiro GetFinanceiroById(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            ItemFinanceiro financeiro = dataAccess.GetFinanceiroById(id);
            dataAccess.CloseConnection();
            return financeiro;
        }

        public Lote GetUltimoLote(string raca, string idPropriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Lote lote = dataAccess.GetUltimoLote(raca, idPropriedade);
            dataAccess.CloseConnection();
            return lote;
        }

        public void RemoveMatriz(int cdcId, int matrizId)
        {

            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveMatriz(cdcId, matrizId);
            dataAccess.CloseConnection();
        }

        public List<Matriz> GetMatrizes(int loteId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Matriz> matrizes = dataAccess.GetMatrizes(loteId);
            dataAccess.CloseConnection();
            return matrizes;
        }

        public Matriz GetMatrizByIndex(int loteId, int matInd)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Matriz matriz = dataAccess.GetMatrizById(loteId, matInd);
            dataAccess.CloseConnection();
            return matriz;
        }

        public void AdicionaMatriz(int cdcId, Matriz matriz)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaMatriz(cdcId, matriz);
            dataAccess.CloseConnection();
        }

        public void SalvaMatriz(int cdcId, int matrizId, Matriz matriz)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaMatriz(cdcId, matrizId, matriz);
            dataAccess.CloseConnection();
        }

        public List<Propriedade> GetPropriedades()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Propriedade> propriedades = dataAccess.GetPropriedades();
            dataAccess.CloseConnection();
            return propriedades;
        }

        public List<CentroCusto> GetCentrosCusto()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<CentroCusto> centrosCusto = dataAccess.GetCentrosCusto();
            dataAccess.CloseConnection();
            return centrosCusto;
        }

        public bool LoteExists(string lote)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.LoteExists(lote);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool PluviometriaExists(string diretorio, DateTime data, string pluviometro)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.PluviometriaExists(diretorio, data, pluviometro);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool LoteExists(string lote, DateTime dataControle)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.LoteExists(lote, dataControle);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool LotePonderalExists(string lotePonderal, DateTime dataControle)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.LotePonderalExists(lotePonderal, dataControle);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool EstruturaPropriedadeExists(string idPropriedade, DateTime data)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.EstruturaPropriedadeExists(idPropriedade, data);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool CdcExists(string cdc)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.CdcExists(cdc);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool CnpjCpfExists(string cnpjCpf)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.CnpjCpfExists(cnpjCpf);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool AnimalLotePonderalExists(string id, string idAnimal)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.AnimalLotePonderalExists(id, idAnimal);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool PastagemEstruturaPropriedadeExists(string id, string tipo)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.PastagemEstruturaPropriedadeExists(id, tipo);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool AgriculturaEstruturaPropriedadeExists(string id, string tipo)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.AgriculturaEstruturaPropriedadeExists(id, tipo);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool BenfeitoriaEstruturaPropriedadeExists(string id, string tipo)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.BenfeitoriaEstruturaPropriedadeExists(id, tipo);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool ArrendamentoEstruturaPropriedadeExists(string id, string tipo)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.ArrendamentoEstruturaPropriedadeExists(id, tipo);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool OutraEstruturaPropriedadeExists(string id, string tipo)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.OutraEstruturaPropriedadeExists(id, tipo);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool MatrizControleLeiteiroExists(string id, string idMatriz)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.MatrizControleLeiteiroExists(id, idMatriz);
            dataAccess.CloseConnection();
            return existe;
        }

        public bool MatrizCdcExists(string id, string idMatriz)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool existe = dataAccess.MatrizCdcExists(id, idMatriz);
            dataAccess.CloseConnection();
            return existe;
        }

        public void SalvaLote(Lote lote)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaLote(lote);
            dataAccess.CloseConnection();
        }

        public long AdicionaLote(Lote lote)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            long ultimoId = dataAccess.AdicionaLote(lote);
            dataAccess.CloseConnection();
            return ultimoId;
        }

        public string GetNomePropriedade(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            string nomePropriedade = dataAccess.GetNomePropriedade(id);
            dataAccess.CloseConnection();
            return nomePropriedade;
        }

        public string GetDescricaoCentroCusto(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            string centroCusto = dataAccess.GetDescricaoCentroCusto(id);
            dataAccess.CloseConnection();
            return centroCusto;
        }

        public string GetNomeAnimal(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            string nomeAnimal = dataAccess.GetNomeAnimal(id);
            dataAccess.CloseConnection();
            return nomeAnimal;
        }

        public ControleLeiteiro GetControleLeiteiroById(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            ControleLeiteiro controleLeiteiro = dataAccess.GetControleLeiteiroById(id);
            dataAccess.CloseConnection();
            return controleLeiteiro;
        }

        public void AdicionaControleLeiteiro(ControleLeiteiro controleLeiteiro)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaControleLeiteiro(controleLeiteiro);
            dataAccess.CloseConnection();
        }

        public void SalvaControleLeiteiro(ControleLeiteiro controleLeiteiro)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaControleLeiteiro(controleLeiteiro);
            dataAccess.CloseConnection();
        }

        public void RemoveControleLeiteiro(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveControleLeiteiro(id);
            dataAccess.CloseConnection();
        }

        public List<ControleLeiteiro> GetControlesById(string idLote)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<ControleLeiteiro> controles = dataAccess.GetControlesById(idLote);
            dataAccess.CloseConnection();
            return controles;
        }

        public List<Animal> GetCrias(string maeId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetCrias(maeId);
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetCriasControleLeiteiro(string maeId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetCriasControleLeiteiro(maeId);
            dataAccess.CloseConnection();
            return animais;
        }

        public List<Animal> GetCriasControleLeiteiro(string maeId, DateTime dataControle)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<Animal> animais = dataAccess.GetCriasControleLeiteiro(maeId, dataControle);
            dataAccess.CloseConnection();
            return animais;
        }

        public ProducaoLeite GetProducaoLeiteByIndex(int controleLeiteiroId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            ProducaoLeite pl = dataAccess.GetProducaoLeiteById(controleLeiteiroId, index);
            dataAccess.CloseConnection();
            return pl;
        }

        public List<ProducaoLeite> GetProducaoLeite(int controleLeiteiroId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetProducaoLeite(controleLeiteiroId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void SalvaProducaoLeite(int controleLeiteiroId, int producaoLeiteId, ProducaoLeite producaoLeite, ProducaoLeite prodLeiteAnterior)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaProducaoLeite(controleLeiteiroId, producaoLeiteId, producaoLeite, prodLeiteAnterior);
            dataAccess.CloseConnection();
        }

        public void AdicionaProducaoLeite(int controleLeiteiroId, ProducaoLeite producaoLeite)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaProducaoLeite(controleLeiteiroId, producaoLeite);
            dataAccess.CloseConnection();
        }

        public void RemoveProducaoLeite(int controleLeiteiroId, int producaoLeiteId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveProducaoLeite(controleLeiteiroId, producaoLeiteId);
            dataAccess.CloseConnection();
        }

        public ProducaoLeite[] ObtemPesagensLeite(CriterioPesquisaPesagensLeite criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemPesagensLeite(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void AlterarPesagemLeite(int loteId, int pesagemLeiteId, ProducaoLeite pesagemLeite)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AlterarPesagemLeite(loteId, pesagemLeiteId, pesagemLeite);
            dataAccess.CloseConnection();
        }


        public List<ProducaoLeite> GetPesagemLeite(int loteID)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetPesagemLeite(loteID);
            dataAccess.CloseConnection();
            return retVal;
        }

        public ProducaoLeite GetPesagemLeiteByIndex(int loteID, int pesagemLeiteId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            ProducaoLeite p = dataAccess.GetPesagemLeiteById(loteID, pesagemLeiteId);
            dataAccess.CloseConnection();
            return p;
        }

        public void EncerrarPesagensLeite(long idLote)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.EncerrarPesagensLeite(idLote);
            dataAccess.CloseConnection();
        }

        public DateTime GetDataNascimentoCria(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            DateTime dataNascimentoCria = dataAccess.GetDataNascimentoCria(id);
            dataAccess.CloseConnection();
            return dataNascimentoCria;
        }

        public DateTime? GetDataEntradaControle(string idLote, string idMatriz, string idCria)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            DateTime? dataEntradaControle = dataAccess.GetDataEntradaControle(idLote, idMatriz, idCria);
            dataAccess.CloseConnection();
            return dataEntradaControle;
        }

        public void RemoveLote(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveLote(id);
            dataAccess.CloseConnection();
        }

        public List<ResultadoBuscaControlePluviometrico> ConsultaControlePluviometrico(ParametrosDeBuscaEmControlePluviometrico parametrosBuscaEmControlePluviometrico)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<ResultadoBuscaControlePluviometrico> controles = dataAccess.ConsultaControlePluviometrico(parametrosBuscaEmControlePluviometrico);
            dataAccess.CloseConnection();
            return controles;
        }

        public ControlePluviometrico GetControlePluviometricoById(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            ControlePluviometrico controlePluviometrico = dataAccess.GetControlePluviometricoById(id);
            dataAccess.CloseConnection();
            return controlePluviometrico;
        }

        public void SalvaControlePluviometrico(ControlePluviometrico controlePluviometrico)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaControlePluviometrico(controlePluviometrico);
            dataAccess.CloseConnection();
        }

        public void AdicionaControlePluviometrico(ControlePluviometrico controlePluviometrico)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaControlePluviometrico(controlePluviometrico);
            dataAccess.CloseConnection();
        }

        public void RemoveControlePluviometrico(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveControlePluviometrico(id);
            dataAccess.CloseConnection();
        }

        public List<ResultadoBuscaCdc> ConsultaCdc(ParametrosDeBuscaEmCdc parametrosBuscaEmCdc)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<ResultadoBuscaCdc> cdcs = dataAccess.ConsultaCdc(parametrosBuscaEmCdc);
            dataAccess.CloseConnection();
            return cdcs;
        }

        public void RemoveCdc(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveCdc(id);
            dataAccess.CloseConnection();
        }



        public Matriz GetCdcMatrizByIndex(int cdcId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Matriz m = dataAccess.GetMatrizById(cdcId, index);
            dataAccess.CloseConnection();
            return m;
        }

        public Cdc GetCdcById(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Cdc cdc = dataAccess.GetCdcById(id);
            dataAccess.CloseConnection();
            return cdc;
        }

        public void SalvaCdc(Cdc cdc)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaCdc(cdc);
            dataAccess.CloseConnection();
        }

        public long AdicionaCdc(Cdc cdc)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            long ultimoId = dataAccess.AdicionaCdc(cdc);
            dataAccess.CloseConnection();
            return ultimoId;
        }

        public List<RAnimaisProducaoLeite> ConsultaProducaoDiariaMedia(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RAnimaisProducaoLeite> animaisProducaoLeite = dataAccess.ConsultaProducaoDiariaMedia(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return animaisProducaoLeite;
        }

        public List<RAnimaisProducaoLeite> ConsultaProducaoDiariaMaxima(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RAnimaisProducaoLeite> animaisProducaoLeite = dataAccess.ConsultaProducaoDiariaMaxima(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return animaisProducaoLeite;
        }

        public List<RAnimaisProducaoLeite> ConsultaProducaoAcumulada(ParametrosDeBuscaEmAnimais parametrosBuscaEmAnimais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RAnimaisProducaoLeite> animaisProducaoLeite = dataAccess.ConsultaProducaoAcumulada(parametrosBuscaEmAnimais);
            dataAccess.CloseConnection();
            return animaisProducaoLeite;
        }

        public List<ResultadoBuscaFluxoCaixa> ConsultaFluxoCaixa(ParametrosDeBuscaEmFluxoCaixa parametrosBuscaEmFluxoCaixa)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<ResultadoBuscaFluxoCaixa> fluxos = dataAccess.ConsultaFluxoCaixa(parametrosBuscaEmFluxoCaixa);
            dataAccess.CloseConnection();
            return fluxos;
        }

        public void RemoveFluxoCaixa(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveFluxoCaixa(id);
            dataAccess.CloseConnection();
        }

        public FluxoCaixa GetFluxoCaixaById(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            FluxoCaixa fluxoCaixa = dataAccess.GetFluxoCaixaById(id);
            dataAccess.CloseConnection();
            return fluxoCaixa;
        }

        public void AdicionaFluxoCaixa(FluxoCaixa fluxoCaixa)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaFluxoCaixa(fluxoCaixa);
            dataAccess.CloseConnection();
        }

        public void SalvaFluxoCaixa(FluxoCaixa fluxoCaixa)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaFluxoCaixa(fluxoCaixa);
            dataAccess.CloseConnection();
        }

        public long UltimoLote(string matrizId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            long ultimoLote = dataAccess.UltimoLote(matrizId);
            dataAccess.CloseConnection();
            return ultimoLote;
        }

        public List<RProducaoLeite> GetPesagens(DateTime dataControle, string matrizId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RProducaoLeite> producaoLeite = dataAccess.GetPesagens(dataControle, matrizId);
            dataAccess.CloseConnection();
            return producaoLeite;
        }

        public List<RProducaoLeite> GetPesagensRebanho(ParametrosDeBuscaEmLotes parametros)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RProducaoLeite> producaoLeite = dataAccess.GetPesagensRebanho(parametros);
            dataAccess.CloseConnection();
            return producaoLeite;
        }

        public List<RProducaoReal> GetProducaoReal(DateTime dataControle, string matrizId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<RProducaoReal> producaoReal = dataAccess.GetProducaoReal(dataControle, matrizId);
            dataAccess.CloseConnection();
            return producaoReal;
        }

        public Producao GetProducaoAcumuladaMatriz(string matrizId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Producao producao = dataAccess.GetProducaoAcumuladaMatriz(matrizId);
            dataAccess.CloseConnection();
            return producao;
        }

        public long AdicionaLotePonderal(LotePonderal lotePonderal)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            long ultimoId = dataAccess.AdicionaLotePonderal(lotePonderal);
            dataAccess.CloseConnection();
            return ultimoId;
        }

        public List<ResultadoBuscaLotePonderal> ConsultaLotePonderal(ParametrosDeBuscaEmLotesPonderais parametrosBuscaEmLotesPonderais)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<ResultadoBuscaLotePonderal> lotesPonderais = dataAccess.ConsultaLotePonderal(parametrosBuscaEmLotesPonderais);
            dataAccess.CloseConnection();
            return lotesPonderais;
        }

        public LotePonderal GetLotePonderalById(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            LotePonderal lotePonderal = dataAccess.GetLotePonderalById(id);
            dataAccess.CloseConnection();
            return lotePonderal;
        }

        public List<LotePonderal> GetLotesPonderais()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<LotePonderal> lotes = dataAccess.GetLotesPonderais();
            dataAccess.CloseConnection();
            return lotes;
        }

        public List<LotePonderal> GetLotesPonderaisParaMensuracoes()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<LotePonderal> lotes = dataAccess.GetLotesPonderaisParaMensuracoes();
            dataAccess.CloseConnection();
            return lotes;
        }

        public void EncerrarMensuracoes(long idLotePonderal)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.EncerrarMensuracoes(idLotePonderal);
            dataAccess.CloseConnection();
        }

        public void RemoveLotePonderal(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveLotePonderal(id);
            dataAccess.CloseConnection();
        }

        public void SalvaLotePonderal(LotePonderal lotePonderal)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaLotePonderal(lotePonderal);
            dataAccess.CloseConnection();
        }

        public void SalvaEstruturaPropriedade(EstruturaPropriedade estruturaPropriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaEstruturaPropriedade(estruturaPropriedade);
            dataAccess.CloseConnection();
        }

        public void SalvaMensuracao(int controlePonderalId, Mensuracao mensuracao, int mensuracaoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaMensuracao(controlePonderalId, mensuracao, mensuracaoId);
            dataAccess.CloseConnection();
        }

        public void AdicionaMensuracao(string controlePonderalId, Mensuracao mensuracao)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaMensuracao(controlePonderalId, mensuracao);
            dataAccess.CloseConnection();
        }

        public Mensuracao GetMensuracaoByIndex(int controlePonderalId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Mensuracao m = dataAccess.GetMensuracaoById(controlePonderalId, index);
            dataAccess.CloseConnection();
            return m;
        }

        public void AdicionaMensuracaoAnimal(string animalId, Mensuracao mensuracao)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaMensuracaoAnimal(animalId, mensuracao);
            dataAccess.CloseConnection();
        }

        public void SalvaMensuracaoAnimal(int animalId, Mensuracao mensuracao, int mensuracaoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaMensuracaoAnimal(animalId, mensuracao, mensuracaoId);
            dataAccess.CloseConnection();
        }

        public Mensuracao GetMensuracaoAnimalByIndex(int animalId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Mensuracao m = dataAccess.GetMensuracaoAnimalById(animalId, index);
            dataAccess.CloseConnection();
            return m;
        }

        public List<Mensuracao> GetMensuracaoAnimal(int animalId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetMensuracaoAnimal(animalId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void RemoveMensuracaoAnimal(int animalId, int mensuracaoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveMensuracaoAnimal(animalId, mensuracaoId);
            dataAccess.CloseConnection();
        }

        public List<RMensuracao> GetMensuracoes(DateTime dataControle, string animalId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetMensuracoes(dataControle, animalId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RMensuracao> GetMensuracoesPes(int loteControlePonderalID)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetMensuracoesPes(loteControlePonderalID);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<Mensuracao> GetMensuracao(int controlePonderalId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetMensuracao(controlePonderalId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void RemoveMensuracao(int controlePonderalId, int mensuracaoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveMensuracao(controlePonderalId, mensuracaoId);
            dataAccess.CloseConnection();
        }

        public long UltimoLotePonderal(string animalId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            long ultimoLote = dataAccess.UltimoLotePonderal(animalId);
            dataAccess.CloseConnection();
            return ultimoLote;
        }

        public void AtualizaDiasLactacao()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AtualizaDiasLactacao();
            dataAccess.CloseConnection();
        }

        public int calculaIdadeAnos(DateTime dNascimento, DateTime dAtual)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            int nomePropriedade = dataAccess.calculaIdadeAnos(dNascimento, dAtual);
            dataAccess.CloseConnection();
            return nomePropriedade;
        }

        public double GetFatorCorrecao(int diasLactacao, int anos)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            double fatorCorrecao = dataAccess.GetFatorCorrecao(diasLactacao, anos);
            dataAccess.CloseConnection();
            return fatorCorrecao;
        }

        public void AtualizaRemocaoControleLeiteiro(string idMatriz, DateTime dataControle, string raca)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AtualizaRemocaoControleLeiteiro(idMatriz, dataControle, raca);
            dataAccess.CloseConnection();
        }

        public bool TemCriasAnoPecuarioAnterior(string idMatriz, DateTime dataCobertura)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool temCrias = dataAccess.TemCriasAnoPecuarioAnterior(idMatriz, dataCobertura);
            dataAccess.CloseConnection();
            return temCrias;
        }

        public List<ResultadoBuscaEstruturaPropriedade> ConsultaEstruturaPropriedade(ParametrosDeBuscaEmEstruturaPropriedades parametrosBuscaEmEstruturaPropriedades)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            List<ResultadoBuscaEstruturaPropriedade> estruturas = dataAccess.ConsultaEstruturaPropriedade(parametrosBuscaEmEstruturaPropriedades);
            dataAccess.CloseConnection();
            return estruturas;
        }

        public void RemoveEstruturaPropriedade(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveEstruturaPropriedade(id);
            dataAccess.CloseConnection();
        }

        public void SalvaPastagem(int estruturaPropriedadeId, Pastagem pastagem, int pastagemId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaPastagem(estruturaPropriedadeId, pastagem, pastagemId);
            dataAccess.CloseConnection();
        }

        public void AdicionaPastagem(string id, Pastagem pastagem)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaPastagem(id, pastagem);
            dataAccess.CloseConnection();
        }

        public Pastagem GetPastagemByIndex(int estruturaPropriedadeId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Pastagem p = dataAccess.GetPastagemById(estruturaPropriedadeId, index);
            dataAccess.CloseConnection();
            return p;
        }

        public List<Pastagem> GetPastagem(int estruturaPropriedadeId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetPastagem(estruturaPropriedadeId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void RemovePastagem(int estruturaPropriedadeId, int pastagemId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemovePastagem(estruturaPropriedadeId, pastagemId);
            dataAccess.CloseConnection();
        }

        public EstruturaPropriedade GetEstruturaPropriedadeById(string id)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            EstruturaPropriedade estruturaPropriedade = dataAccess.GetEstruturaPropriedadeById(id);
            dataAccess.CloseConnection();
            return estruturaPropriedade;
        }

        public EstruturaPropriedade GetEstruturaPropriedade(string id, DateTime dataInicio, DateTime dataFim)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            EstruturaPropriedade estruturaPropriedade = dataAccess.GetEstruturaPropriedade(id, dataInicio, dataFim);
            dataAccess.CloseConnection();
            return estruturaPropriedade;
        }

        public long AdicionaEstruturaPropriedade(EstruturaPropriedade estruturaPropriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var ultimoId = dataAccess.AdicionaEstruturaPropriedade(estruturaPropriedade);
            dataAccess.CloseConnection();
            return ultimoId;
        }

        public void SalvaAgricultura(int estruturaPropriedadeId, Agricultura agricultura, int agriculturaId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaAgricultura(estruturaPropriedadeId, agricultura, agriculturaId);
            dataAccess.CloseConnection();
        }

        public void AdicionaAgricultura(string id, Agricultura agricultura)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaAgricultura(id, agricultura);
            dataAccess.CloseConnection();
        }

        public Agricultura GetAgriculturaByIndex(int estruturaPropriedadeId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Agricultura a = dataAccess.GetAgriculturaById(estruturaPropriedadeId, index);
            dataAccess.CloseConnection();
            return a;
        }

        public List<Agricultura> GetAgriculturas(int estruturaPropriedadeId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetAgriculturas(estruturaPropriedadeId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void RemoveAgricultura(int estruturaPropriedadeId, int agriculturaId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveAgricultura(estruturaPropriedadeId, agriculturaId);
            dataAccess.CloseConnection();
        }


        public void SalvaBenfeitoria(int estruturaPropriedadeId, Benfeitoria benfeitoria, int benfeitoriaId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaBenfeitoria(estruturaPropriedadeId, benfeitoria, benfeitoriaId);
            dataAccess.CloseConnection();
        }

        public void AdicionaBenfeitoria(string id, Benfeitoria benfeitoria)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaBenfeitoria(id, benfeitoria);
            dataAccess.CloseConnection();
        }

        public Benfeitoria GetBenfeitoriaByIndex(int estruturaPropriedadeId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Benfeitoria b = dataAccess.GetBenfeitoriaById(estruturaPropriedadeId, index);
            dataAccess.CloseConnection();
            return b;
        }

        public List<Benfeitoria> GetBenfeitorias(int estruturaPropriedadeId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetBenfeitorias(estruturaPropriedadeId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void RemoveBenfeitoria(int estruturaPropriedadeId, int benfeitoriaId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveBenfeitoria(estruturaPropriedadeId, benfeitoriaId);
            dataAccess.CloseConnection();
        }

        public void SalvaArrendamento(int estruturaPropriedadeId, Arrendamento arrendamento, int arrendamentoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaArrendamento(estruturaPropriedadeId, arrendamento, arrendamentoId);
            dataAccess.CloseConnection();
        }

        public void AdicionaArrendamento(string id, Arrendamento arrendamento)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaArrendamento(id, arrendamento);
            dataAccess.CloseConnection();
        }

        public Arrendamento GetArrendamentoByIndex(int estruturaPropriedadeId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Arrendamento arr = dataAccess.GetArrendamentoById(estruturaPropriedadeId, index);
            dataAccess.CloseConnection();
            return arr;
        }

        public List<Arrendamento> GetArrendamentos(int estruturaPropriedadeId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetArrendamentos(estruturaPropriedadeId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void RemoveArrendamento(int estruturaPropriedadeId, int arrendamentoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveArrendamento(estruturaPropriedadeId, arrendamentoId);
            dataAccess.CloseConnection();
        }

        public void SalvaOutra(int estruturaPropriedadeId, Outra outra, int outraId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.SalvaOutra(estruturaPropriedadeId, outra, outraId);
            dataAccess.CloseConnection();
        }

        public void AdicionaOutra(string id, Outra outra)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AdicionaOutra(id, outra);
            dataAccess.CloseConnection();
        }

        public Outra GetOutraByIndex(int estruturaPropriedadeId, int index)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Outra o = dataAccess.GetOutraById(estruturaPropriedadeId, index);
            dataAccess.CloseConnection();
            return o;
        }

        public List<Outra> GetOutras(int estruturaPropriedadeId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetOutras(estruturaPropriedadeId);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void RemoveOutra(int estruturaPropriedadeId, int OutraId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoveOutra(estruturaPropriedadeId, OutraId);
            dataAccess.CloseConnection();
        }

        public double GetMediaPluviometria(DateTime data, string propriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            double media = dataAccess.GetMediaPluviometria(data, propriedade);
            dataAccess.CloseConnection();
            return media;
        }

        public List<IndiceZootecnico> LotacaoMediaAnual(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.LotacaoMediaAnual(dataInicio, dataFim, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> TaxaCrescimentoVegetativo(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.TaxaCrescimentoVegetativo(dataInicio, dataFim, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> TaxaVendas(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.TaxaVendas(dataInicio, dataFim, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> TaxaMortalidade(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda, string categoria)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.TaxaMortalidade(dataInicio, dataFim, raca, idFazenda, categoria);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> TaxaAbate(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.TaxaAbate(dataInicio, dataFim, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> TaxaDesfrute(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.TaxaDesfrute(dataInicio, dataFim, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> ProducaoCarne(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ProducaoCarne(dataInicio, dataFim, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> MatrizesNBezerros(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda, double nBezerros)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.MatrizesNBezerros(dataInicio, dataFim, raca, idFazenda, nBezerros);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> IndiceFertilidade(string anoPecuario, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.IndiceFertilidade(anoPecuario, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> IndiceFertilidade(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.IndiceFertilidade(dataInicio, dataFim, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> TaxaNatalidade(string anoPecuario, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.TaxaNatalidade(anoPecuario, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> TaxaMortalidadeIntrauterina(string anoPecuario, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.TaxaMortalidadeIntrauterina(anoPecuario, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> TaxaDesmame(string anoPecuario, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.TaxaDesmame(anoPecuario, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> RelacaoDesmama(string anoPecuario, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.RelacaoDesmama(anoPecuario, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> TodasTaxas(string anoPecuario, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.TodasTaxas(anoPecuario, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<IndiceZootecnico> RQMedio(DateTime dataInicio, DateTime dataFim, string raca, string idFazenda)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.RQMedio(dataInicio, dataFim, raca, idFazenda);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<ProducaoLeite> ProducoesLeiteEnc(int ano, string idFazenda, string raca)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ProducoesLeiteEnc(ano, idFazenda, raca);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<ProducaoLeite> ProducoesLeiteEnc(int ano, string idAnimal)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ProducoesLeiteEnc(ano, idAnimal);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<ProducaoLeite> ProducoesLeiteEnc(int ano, string idFazenda, string raca, string tipo)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ProducoesLeiteEnc(ano, idFazenda, raca, tipo);
            dataAccess.CloseConnection();
            return retVal;
        }

        public GrupoFinanceiro[] ObtemGrupos(string criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemGrupos(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemItensFinanceiros(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemItensFinanceiros(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemVendasAnimais(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemVendasAnimais(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public InformacoesFinanceiras ObtemInformacoesFinanceiras(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemInformacoesFinanceiras(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<InformacoesFinanceiras> ObtemListaInformacoesFinanceiras(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemListaInformacoesFinanceiras(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemItensFinanceirosCompleto(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemItensFinanceirosCompleto(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemCustosItensFinanceiros(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemCustosItensFinanceiros(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemCustosFixos(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemCustosFixos(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemCustosVariaveis(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemCustosVariaveis(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemBalancoFinanceiro(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemBalancoFinanceiro(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemInvestimentos(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemInvestimentos(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemCustos(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemCustos(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public List<RItemFinanceiro> ObtemCusteioTotalXInvestimentos(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemCusteioTotalXInvestimentos(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public GrupoFinanceiro[] ObtemFilhosDe(int idGrupo)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemFilhosDe(idGrupo);
            dataAccess.CloseConnection();
            return retVal;
        }

        public GrupoFinanceiro[] ObtemGrupo(long idGrupo, bool comHierarquia)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemGrupo(idGrupo, comHierarquia);
            dataAccess.CloseConnection();
            return retVal;
        }

        public Propriedade[] ObtemPropriedades()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemPropriedades();
            dataAccess.CloseConnection();
            return retVal;
        }

        public Propriedade ObtemPropriedade(int idPropriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemPropriedade(idPropriedade);
            dataAccess.CloseConnection();
            return retVal;
        }

        public Propriedade ObtemPropriedadeCompleta(int idPropriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemPropriedadeCompleta(idPropriedade);
            dataAccess.CloseConnection();
            return retVal;
        }

        public Propriedade ObtemPropriedadeComp(string idsPropriedadesComp)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemPropriedadeComp(idsPropriedadesComp);
            dataAccess.CloseConnection();
            return retVal;
        }

        public Propriedade[] ObtemPropriedadesComp()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemPropriedadesComp();
            dataAccess.CloseConnection();
            return retVal;
        }

        public Empresa ObtemEmpresa(int idEmpresa)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemEmpresa(idEmpresa);
            dataAccess.CloseConnection();
            return retVal;
        }

        public ItemFinanceiro[] ObtemFinanceiros(string criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemFinanceiros(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public ItemFinanceiro[] ConsultaFinanceiro(CriterioPesquisaFinanceiro criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ConsultaFinanceiro(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public long InserirFinanceiro(ItemFinanceiro financeiro)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            long ultimoId = dataAccess.InserirFinanceiro(financeiro);
            dataAccess.CloseConnection();

            return ultimoId;
        }

        public void AlterarFinanceiro(ItemFinanceiro financeiro)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AlterarFinanceiro(financeiro);
            dataAccess.CloseConnection();
        }

        public void RemoverFinanceiro(long idFinanceiro)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoverFinanceiro(idFinanceiro);
            dataAccess.CloseConnection();
        }

        public void ValidarFinanceiro(long idFinanceiro, string usuarioValidacao, DateTime usuarioDataValidacao)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.ValidarFinanceiro(idFinanceiro, usuarioValidacao, usuarioDataValidacao);
            dataAccess.CloseConnection();
        }

        public Empresa[] ObtemEmpresas(string criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemEmpresas(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public Empresa[] ObtemEmpresas()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemEmpresas();
            dataAccess.CloseConnection();
            return retVal;
        }

        public Empresa[] ObtemClientes()
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemClientes();
            dataAccess.CloseConnection();
            return retVal;
        }

        public Empresa[] ConsultaEmpresa(CriterioPesquisaEmpresa criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ConsultaEmpresa(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void InserirEmpresa(Empresa empresa)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.InserirEmpresa(empresa);
            dataAccess.CloseConnection();
        }

        public void AlterarEmpresa(Empresa empresa)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AlterarEmpresa(empresa);
            dataAccess.CloseConnection();
        }

        public void RemoverEmpresa(long idEmpresa)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoverEmpresa(idEmpresa);
            dataAccess.CloseConnection();
        }

        public ControlePluviometrico[] ObtemPluviometrias(string criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemPluviometrias(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public ControlePluviometrico[] ConsultaPluviometria(ParametrosDeBuscaEmControlePluviometrico criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ConsultaPluviometria(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void InserirPluviometria(ControlePluviometrico pluviometria)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.InserirPluviometria(pluviometria);
            dataAccess.CloseConnection();
        }

        public void AlterarPluviometria(ControlePluviometrico pluviometria)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AlterarPluviometria(pluviometria);
            dataAccess.CloseConnection();
        }

        public void RemoverPluviometria(long idPluviometria)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoverPluviometria(idPluviometria);
            dataAccess.CloseConnection();
        }

        public Compra[] ObtemCompras(CriterioPesquisaCompras criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemCompras(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void InserirCompra(string itemFinanceiroId, Compra compra)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.InserirCompra(itemFinanceiroId, compra);
            dataAccess.CloseConnection();
        }

        public void AlterarCompra(int itemFinanceiroId, int compraId, Compra compra)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AlterarCompra(itemFinanceiroId, compraId, compra);
            dataAccess.CloseConnection();
        }

        public List<Compra> GetCompra(int itemFinanceiroID)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetCompra(itemFinanceiroID);
            dataAccess.CloseConnection();
            return retVal;
        }

        public Compra GetCompraByIndex(int itemFinanceiroID, int compraId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Compra p = dataAccess.GetCompraById(itemFinanceiroID, compraId);
            dataAccess.CloseConnection();
            return p;
        }

        public void RemoverCompra(int itemFinanceiroId, int compraId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoverCompra(itemFinanceiroId, compraId);
            dataAccess.CloseConnection();
        }



        public Documento[] ObtemDocumentos(CriterioPesquisaDocumentos criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemDocumentos(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void InserirDocumento(string itemFinanceiroId, Documento documento)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.InserirDocumento(itemFinanceiroId, documento);
            dataAccess.CloseConnection();
        }

        public void AlterarDocumento(int itemFinanceiroId, int documentoId, Documento documento)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AlterarDocumento(itemFinanceiroId, documentoId, documento);
            dataAccess.CloseConnection();
        }

        public List<Documento> GetDocumento(int itemFinanceiroID)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetDocumento(itemFinanceiroID);
            dataAccess.CloseConnection();
            return retVal;
        }

        public Documento GetDocumentoByIndex(int itemFinanceiroID, int documentoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Documento d = dataAccess.GetDocumentoById(itemFinanceiroID, documentoId);
            dataAccess.CloseConnection();
            return d;
        }

        public void RemoverDocumento(int itemFinanceiroId, int documentoId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoverDocumento(itemFinanceiroId, documentoId);
            dataAccess.CloseConnection();
        }

        public Parcela[] ObtemParcelas(CriterioPesquisaParcelas criterio)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.ObtemParcelas(criterio);
            dataAccess.CloseConnection();
            return retVal;
        }

        public void InserirParcela(string itemFinanceiroId, Parcela parcela)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.InserirParcela(itemFinanceiroId, parcela);
            dataAccess.CloseConnection();
        }

        public void InserirParcelas(DateTime data, double valorTotal, int nParcelas, long itemFinanceiroId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.InserirParcelas(data, valorTotal, nParcelas, itemFinanceiroId);
            dataAccess.CloseConnection();
        }

        public void AlterarParcela(int itemFinanceiroId, int parcelaId, Parcela parcela)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.AlterarParcela(itemFinanceiroId, parcelaId, parcela);
            dataAccess.CloseConnection();
        }

        public List<Parcela> GetParcela(int itemFinanceiroID)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetParcela(itemFinanceiroID);
            dataAccess.CloseConnection();
            return retVal;
        }

        public Parcela GetParcelaByIndex(int itemFinanceiroID, int parcelaId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            Parcela p = dataAccess.GetParcelaById(itemFinanceiroID, parcelaId);
            dataAccess.CloseConnection();
            return p;
        }

        public void RemoverParcela(int itemFinanceiroId, int parcelaId)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            dataAccess.RemoverParcela(itemFinanceiroId, parcelaId);
            dataAccess.CloseConnection();
        }

        public InformacoesRebanho GetInformacoesRebanho(string raca)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            InformacoesRebanho informacoesRebanho = dataAccess.GetInformacoesRebanho(raca);
            dataAccess.CloseConnection();
            return informacoesRebanho;
        }

        public InformacoesRebanho GetInformacoesRebanho(string raca, string propriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            InformacoesRebanho informacoesRebanho = dataAccess.GetInformacoesRebanho(raca, propriedade);
            dataAccess.CloseConnection();
            return informacoesRebanho;
        }

        public double[] GetTodosGmds(DateTime dataInicio, DateTime dataFim, string idPropriedade)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetTodosGmds(dataInicio, dataFim, idPropriedade);
            dataAccess.CloseConnection();
            return retVal;
        }

        public bool ItemFinanceiroValidado(long idFinanceiro)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            bool validado = dataAccess.ItemFinanceiroValidado(idFinanceiro);
            dataAccess.CloseConnection();
            return validado;
        }

        public string GetNomeArquivoOriginal(string nomeArquivoGerado)
        {
            FCarnaubaDataAccess dataAccess = new FCarnaubaDataAccess();
            dataAccess.OpenConnection();
            var retVal = dataAccess.GetNomeArquivoOriginal(nomeArquivoGerado);
            dataAccess.CloseConnection();
            return retVal;
        }
    }
}
