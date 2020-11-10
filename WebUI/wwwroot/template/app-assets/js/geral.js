/*
 * 
 * Lucansoft.co.ao
 * Javascript:time-data controller
 * create by cristovjobs 
 * 01/11/2019
 * 
 * 
 */

function time() {

    //lista de meses
    meses = new Array("Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro",
            "Outubro", "Novembro", "Dezembro");

    today = new Date();
    h = today.getHours();
    m = today.getMinutes();

    //oumenta 0 no minuto
    m = m > 9 ? m : '0' + m;

    //oumenta 0 na hora quando 
    h = h > 9 ? h : '0' + h;


    //lista da semana
    var dia = today.getDay();
    var semana = new Array(6);
    semana[0] = 'Domingo';
    semana[1] = 'Segunda-Feira';
    semana[2] = 'Terça-Feira';
    semana[3] = 'Quarta-Feia';
    semana[4] = 'Quinta-Feira';
    semana[5] = 'Sexta-Feira';
    semana[6] = 'Sábado';


    s = today.getSeconds();

    document.getElementById('txt').innerHTML = h + ":" + m + " ";
    //document.getElementById('teste').innerHTML = h + ":" + m + " ";

    saudacoes = new Date();
    periodo = saudacoes.getHours();
    if (periodo < 5) {

        if (semana[dia] == 'Domingo' || semana[dia] == 'Sábado') {
            document.getElementById('saudacoes').innerHTML = "<i class='fa fa-circle' style='color: #f44336'></i> Mercado Fechado";
          

        } else {
            document.getElementById('saudacoes').innerHTML = "<i class='fa fa-circle' style='color: #f44336'></i> Mercado Fechado";
            

        }

    } else
    if (periodo < 8) {

        if (semana[dia] == 'Domingo' || semana[dia] == 'Sábado') {

            document.getElementById('saudacoes').innerHTML = "<i class='fa fa-circle' style='color: #f44336'></i> Mercado Fechado";
        

        } else {
            document.getElementById('saudacoes').innerHTML = "<i class='fa fa-circle' style='color: #76FF03'></i> Mercado Aberto";
            document.getElementById('mobile-saudacao').innerHTML = "<i class='fa fa-circle' style='color: #76FF03'></i> Mercado Aberto";

        }

    } else
    if (periodo < 12) {

        if (semana[dia] == 'Domingo' || semana[dia] == 'Sábado') {
            document.getElementById('saudacoes').innerHTML = "<i class='fa fa-circle' style='color: #f44336'></i> Mercado Fechado";
         

        } else {
            document.getElementById('saudacoes').innerHTML = "<i class='fa fa-circle' style='color: #76FF03'></i> Mercado Aberto";
             
        }

    } else
    if (periodo < 18) {

        if (semana[dia] == 'Domingo' || semana[dia] == 'Sábado') {
            document.getElementById('saudacoes').innerHTML = "<i class='fa fa-circle' style='color: #f44336'></i> Mercado Fechado";
          

        } else {
            document.getElementById('saudacoes').innerHTML = "<i class='fa fa-circle' style='color: #76FF03'></i> Mercado Aberto";
            

        }

    } else {

        if (semana[dia] == 'Domingo' || semana[dia] == 'Sábado') {
            document.getElementById('saudacoes').innerHTML = "<i class='fa fa-circle' style='color: #f44336'></i> Mercado Fechado";
         

        } else {
            document.getElementById('saudacoes').innerHTML = " <i class='fa fa-circle' style='color: #f44336'></i> Mercado Fechado";
           
        }

    }

    hoje = new Date();
    dia = hoje.getDate();
    dias = hoje.getDay();
    mes = hoje.getMonth();
    ano = hoje.getYear();
    if (navigator.appName == "Netscape")
      ano = ano + 1900;
    // document.getElementById('date').innerHTML = semana[dias] + ", " + dia + " de " + meses[mes] + " de " + ano;
    setTimeout('time()', 500);

}



