using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
namespace ApiUnity.Controllers;
using System.Data;

[ApiController]
[Route("[controller]")]
public class OxxoController : ControllerBase
{
    string connectionString = "placeholder";

    // Devuelve una pregunta con el id
    [Route("GetPreguntaConId/{idPreg}")]
    [HttpGet]
    public PreguntaMariposa GetPreguntaConId(int idPreg) {
        PreguntaMariposa pregunta = new PreguntaMariposa();
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_EM_getPregunta";
        cmd.Connection = conexion;

        cmd.Parameters.AddWithValue("@idPreg", idPreg);
        using (var reader = cmd.ExecuteReader()) {
                if (reader.Read())
                {
                    pregunta.idpregunta = Convert.ToInt32(reader["idpregunta"]);
                    pregunta.pregunta = reader["pregunta"].ToString();
                    pregunta.respuesta1 = reader["respuesta1"].ToString();
                    pregunta.respuesta2 = reader["respuesta2"].ToString();
                    pregunta.respuesta3 = reader["respuesta3"].ToString();
                    pregunta.correcta = Convert.ToInt32(reader["correcta"]);
                    pregunta.indicadorsubir = Convert.ToInt32(reader["indicadorsubir"]);
                    pregunta.indicadorbajar = Convert.ToInt32(reader["indicadorbajar"]);
                }
            }

        cmd.Prepare();
        cmd.ExecuteNonQuery();
        conexion.Close();

        return pregunta;
    }

    [Route("GetCandidato/{idCand}")]
    [HttpGet]
    public ExpPendCandidato GetCandidato(int idCand) {
        ExpPendCandidato candidato = new ExpPendCandidato();
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_EP_GetCandidato";
        cmd.Connection = conexion;

        cmd.Parameters.AddWithValue("@idCand", idCand);
        using (var reader = cmd.ExecuteReader()) {
                if (reader.Read())
                {
                    candidato.id_candidato = Convert.ToInt32(reader["id_candidato"]);
                    candidato.nombre = reader["nombre"].ToString();
                    candidato.domicilio = reader["domicilio"].ToString();
                    candidato.fecha_nacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]);
                    candidato.nombre_uni = reader["nombre_uni"].ToString();
                    candidato.carrera = reader["carrera"].ToString();
                    candidato.llamada_recomendacion = reader["llamada_recomendacion"].ToString();
                    candidato.trabajos = reader["trabajos"].ToString();
                    candidato.contratar = Convert.ToBoolean(reader["contratar"]);
                }
            }

        cmd.Prepare();
        cmd.ExecuteNonQuery();
        conexion.Close();

        return candidato;
    }

    [Route("UpdateCoins/{coins}/{idUsuario}")]
    [HttpPut]
    public void AgregarPagina(int coins, int idUsuario) {
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_Menu_updateCoins";
        cmd.Connection = conexion;
        cmd.Parameters.AddWithValue("@coins", coins);
        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        conexion.Close();
    }

    [Route("GetInfoMenu/{idUsuario}")]
    [HttpGet]
    public Usuario GetInfoMenu(int idUsuario) {
        Usuario usuario = new Usuario();
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_Menu_GetInfoMenu";
        cmd.Connection = conexion;

        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
        using (var reader = cmd.ExecuteReader()) {
                if (reader.Read())
                {
                    usuario.monedas = Convert.ToInt32(reader["monedas"]);
                    usuario.nivel = Convert.ToInt32(reader["nivel"]);
                }
            }

        cmd.Prepare();
        cmd.ExecuteNonQuery();
        conexion.Close();

        return usuario;
    }

    [Route("VerificarUsuario/{usrName}/{contra}")]
    [HttpGet]
    public bool VerificarUsuario(string usrName, string contra) {
        bool existe = false;
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_Menu_IniciarSesion";
        cmd.Connection = conexion;

        cmd.Parameters.AddWithValue("@usrName", usrName);
        cmd.Parameters.AddWithValue("@contra", contra);
        
        using (var reader = cmd.ExecuteReader()) {
                if (reader.Read())
                {
                    existe = Convert.ToInt32(reader["existe"]) > 0;
                }
            }
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        conexion.Close();

        return existe;
    }

    [Route("GetUsuario/{usrName}")]
    [HttpGet]
    public Usuario GetUsuario(string usrName) {
        Usuario usuario = new Usuario();
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_Menu_GetUsuario";
        cmd.Connection = conexion;

        cmd.Parameters.AddWithValue("@usrName", usrName);
        using (var reader = cmd.ExecuteReader()) {
                if (reader.Read())
                {
                    usuario.id_usuario = Convert.ToInt32(reader["id_usuario"]);
                    usuario.monedas = Convert.ToInt32(reader["monedas"]);
                    usuario.nivel = Convert.ToInt32(reader["nivel"]);
                }
            }

        cmd.Prepare();
        cmd.ExecuteNonQuery();
        conexion.Close();

        return usuario;
    }

    [Route("GetTiendasUsuario/{idUsr}")]
    [HttpGet]
    public List<Tiendas> GetTiendasUsuario(int idUsr) {
        List<Tiendas> listaTiendas = new List<Tiendas>();
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_Menu_GetTiendasUsuario";
        cmd.Connection = conexion;

        Tiendas tienda1 = new Tiendas();
        cmd.Parameters.AddWithValue("@idUsr", idUsr);
        using (var reader = cmd.ExecuteReader()) {
                while (reader.Read())
                {
                    tienda1 = new Tiendas();
                    tienda1.id_franquicia = Convert.ToInt32(reader["id_franquicia"]);
                    tienda1.id_tienda = Convert.ToInt32(reader["id_tienda"]);
                    tienda1.id_usuario = Convert.ToInt32(reader["id_usuario"]);
                    listaTiendas.Add(tienda1);
                }
            }

        cmd.Prepare();
        cmd.ExecuteNonQuery();
        conexion.Close();

        return listaTiendas;
    }

    [Route("ComprarTienda/{idUsr}/{idTiend}")]
    [HttpPost]
    public void ComprarTienda(int idUsr, int idTiend) {
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SP_Menu_ComprarTienda";
        cmd.Connection = conexion;
        cmd.Parameters.AddWithValue("@idUsr", idUsr);
        cmd.Parameters.AddWithValue("@idTiend", idTiend);
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        conexion.Close();
    }    
}
