Api para application Tracking System, um software de gestão de processos de Recrutamento e Seleção que armazena, lê e ranqueia currículos

As configurações desta API podem ser gerenciadas do arquivo appsettings.json

Configurações:

{
  "ConnectionStrings": {
    "ConnectionBase": Conexão do banco da aplicação
  },
  "Blob": {
    "Connection": 'Cadeia de conexão' com o Azure Blob, encontrada na Aba de Configurações / Chaves de Acesso,

    "Folder": pasta criada dentro do Blob para armazenar os arquivos,

    "BaseURL": URL base do Blob, para composição de Url a partir do nome dos arquivos
  }
}

### Deploy

- Build Project


### Prerequisites for Deploy

- Create Acount in Microsoft Azure and app web with Node v14
- Download publishing profile
- Publish with Visual Studio 19, selecting deploy in azure with import publishing profile.
