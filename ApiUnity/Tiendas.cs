public class Tiendas
{
	public int id_franquicia {get;set;}
	public int id_tienda {get;set;}
	public int id_usuario {get;set;}

    public Tiendas() {
        
    }

	public Tiendas(int id_franquicia_,int id_tienda_,int id_usuario_)
	{
		this.id_franquicia = id_franquicia_;
		this.id_tienda = id_tienda_;
		this.id_usuario = id_usuario_;
	}
}