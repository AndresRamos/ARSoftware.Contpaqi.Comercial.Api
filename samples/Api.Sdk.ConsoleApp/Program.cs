using Api.Sdk.ConsoleApp.JsonFactories;

Console.WriteLine("Programa Inicio");

const string baseDirectory = @"C:\AR Software\Contpaqi Comercial API\Requests";

AgenteFactory.CearJson(Path.Combine(baseDirectory, "Agentes"));
AlmacenFactory.CearJson(Path.Combine(baseDirectory, "Almacenes"));
ClienteFactory.CearJson(Path.Combine(baseDirectory, "Clientes"));
ConceptoFactory.CearJson(Path.Combine(baseDirectory, "Conceptos"));
EmpresaFactory.CearJson(Path.Combine(baseDirectory, "Empresas"));
ProductoFactory.CearJson(Path.Combine(baseDirectory, "Productos"));
DocumentoFactory.CearJson(Path.Combine(baseDirectory, "Documentos"));

Console.WriteLine("Programa Fin");
