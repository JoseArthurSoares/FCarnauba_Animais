<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="FCarnauba_Animais_WebMobile.Principal" AspCompat="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Fazenda Carnaúba - Início</title>
    <link rel="icon" type="image/png" href="img/favicon-1.png">
    <link rel="stylesheet" type="text/css" href="./Styles/SiteMobile.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">
    <style>
        
        @import url('https://fonts.googleapis.com/css2?family=Delicious+Handrawn&display=swap');
        @import url('https://fonts.googleapis.com/css2?family=Delicious+Handrawn&family=Delius&display=swap');
        
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
        }

        .header {
            background-color: #002855;
            color: #fff;
            text-align: center;
            padding: 10px 0;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 50px;
            height: 115px;
        }

        .header img {
            width: 330px;
            height: 80px;
        }

        .header h1 {
            font-size: 1.5rem;
            margin: 0;
        }

        .menu {
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-top: 20px;
            
        }
        .container-button
        {
            width: 430px;
           
        }

        .menu-button {
            height: 138px;
            width: 100%;
            background-color: #ffcc00;
            color: #000;
            font-size: 1.2rem;
            border: none;
            border-radius: 8px;
            padding: 15px;
            margin: 10px 0;
            display: flex;
            align-items: center;
            justify-content: flex-start;
            text-decoration: none;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        @media (max-width: 768px) {
            .menu-buttom
            {
                width: 90% !important;
            }
            .container-button
            {
                width: 98%px;
            }
                
        }

        .menu-button img {
            width: 97px;
            height: 111px;
            margin-right: 15px;
        }

        .menu-button:hover {
            background-color: #e6b800;
            text-decoration: none;
        }
        
        .menu-button span
        {
            font-family: "Delicious Handrawn", cursive;
            font-weight: 400;
            font-style: normal;
            font-size: 35px;
        }
        
        footer {
            background-color: #002855;
            color: #fff;
            text-align: center;
            padding: 35px;
            position: fixed;
            width: 100%;
            height: 111px;
            bottom: 0;
        }

        footer img {
            height: 39px;
            margin-left: 13px;
        }
        
        footer span
        {
            font-family: "Delius", cursive;
            font-weight: 400;
            font-style: normal;
        }
        
         .header-logout {
            display: flex;
            align-items: center; 
            gap: 3px; 
            color: white; 
            font-size: 16px; 
            line-height: 1; 
        }

        .header-logout img {
            height: 24px; 
            width: auto;
            display: block; 
        }

        .header-logout span {
            display: flex;
            align-items: center;
            line-height: 1;
            
        }
        .header-logout p
        {
            margin-top:13px;
        }
        
    </style>
</head>
<body>
    <div class="header">
        <a href="https://fazendacarnauba.com/">
            <img src="../img/sgp2.png" alt="Logo da Fazenda">
        </a>
        <div class="header-logout">
          <img src="../img/sair.png" alt="Logo">
          <p>Sair</p>
        </div>
    </div>

    <div class="menu">
        <div class="container-button">
            <a href="Leiteiro.aspx" class="menu-button">
                <img src="../img/icon_milk.png" alt="Controle Leiteiro">
                <span>Controle Leiteiro</span>
            </a>
        </div>
        
        <div class="container-button">
            <a href="Ponderal.aspx" class="menu-button">
                <img src="../img/icon_bal.png" alt="Ponderal">
                <span>Ponderal</span>
            </a>
        </div>
    </div>

    <footer>
            <span>&copy; 2024 - Todos os direitos reservados.</span>
            <img src="../img/logo-lightbase2.png" alt="Logo da Lightbase">
        </footer>
</body>
</html>