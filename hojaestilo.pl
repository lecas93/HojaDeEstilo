<!-- Fichero hojaestilo.pl -->
{LISTADO
<?xml version="1.0" encoding="iso-8859-1"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
{ASIGNATURA
 <head>
   <title>Notas de @NOMBRE@ @CURSO@</title>
 </head>
}ASIGNATURA
<body>
<p style="font-size: 30pt">@@</p>
<br></br>
<br></br>
{ASIGNATURA
     <p>Curso @CURSO@. @NOMBRE@.</p>
}ASIGNATURA
<br></br>
{NOTAS
  <table border="2" cellpadding="10%">
     <caption></caption>
        <tbody>
           <tr>
               <td style="font-weight: bold">DNI</td>
               <td style="font-weight: bold">Calificaci&oacute;n</td>
          </tr>
                 {ALUMNO
                   <tr>
                     <td style="text-align: center">
                         @DNI@
                     </td>
                     <td style="text-align: center">
                         @NOTA@
                     </td>
		     <td style="text-align: center">
                         @NOMBRE@
                     </td>
                  </tr>
                 }ALUMNO
        </tbody>
  </table>
}NOTAS
</body>
</html>
}LISTADO