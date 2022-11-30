EXTERNAL Name(charName)
EXTERNAL Icon(iconName)
EXTERNAL Font(fontName)
EXTERNAL Background(backgroundImage)
EXTERNAL FontStyle(fontStyle)
EXTERNAL FadeIn(speed)
EXTERNAL FadeOut(speed)

{Background("Cuarto")}
{Font("Monica")}
{Name("")}

\*Rrrrrrrrring*
\*Rrrrrrrrring*

{Name("Monica")}
{Icon("Monica/Monica_Pijama_Ome")}

\*Bostezo*

Buenos dias Ome
Despierta pequeño roñoso

{Name("Ome")}
\*Grrrr*

{FontStyle("Normal")}
{Name("Monica")}
Vamos Ome.
Hoy toca desayunar huevito con salchichas, si te portas bien te daré algunas de mi plato.

{Name("")}
\*Ome sale corriendo*

{Name("Monica")}
{Icon("Monica/Monica_Pijama")}
¡Oh, bueno, a ese no hay que decirle las cosas dos veces!
Tengo que preparame para bajar a desayunar

VAR limpia = false
VAR dientes = false
VAR bano = false
VAR cambio = false

-> opciones_limpieza

== opciones_limpieza

    * [Lavarse los dientes]
    ~ dientes = true
    El sabor a menta me devuelve la vida -> opciones_limpieza
    * [Ir al baño]
    ~ bano = true
    Uff, me siento mejor. -> opciones_limpieza
    * [Vestirse]
    ~ cambio = true
    {Icon("Monica_IRL")}
    Bien, ahora podemos ir a desayunar. -> opciones_limpieza
    * [Salir del cuarto]
    {dientes and bano and cambio: 
        ~ limpia = true
    }
    {not cambio:
        Me voy a cambiar antes de bajar
    }
    {FadeOut(2)} -> afuera_cuarto
    
== afuera_cuarto
    {Icon("")}
    {Name("")}
    {Background("Pasillo")}
    {FadeIn(2)}
    ...
    {Name("Santiago")}
    {Icon("Awelo")}
    Buenos dias mi Monita.
    ¿Cómo amaneciste hoy?
    {not limpia: 
        Ya veo que algo apestosilla
        ¿Por qué no vas a alistarte antes de ir a desayunar mi niña?
    }
    
    {Name("Monica")}
    {Icon("Monica/Monica_Sonriendo")}
    \*Beso en la mejilla*
    Bien Santito, dormí perfectamente y Ome como siempre no quería despertar, es un gato muy flojo.
    
    {Name("Santiago")}
    {Icon("Awelo")}
     Si… vi como salío más rápido que el diablo de tu habitación, pero con estos huesos tan viejos, no me fue posible atrapar a la pequeña alimaña
     
    {Name("Monica")}
    {Icon("Monica_IRL")}
    Perdón Santito… te prometo que ya no le voy a dejar salir en las noches.
    Hace cada diablura que hasta a ti que tienes tanta paciencia te desquicia.
    
    {Name("Santiago")}
    {Icon("Awelo")}
    Bueno… lo perdono.
    Pero sólo porque tu me lo pides, porque eres mi mejor nietecita.
    
    {Name("Monica")}
    {Icon("Monica/Monica_Sonriendo")}
    Soy tu única nieta Santito.
    
    {Icon("")}
    {Name("")}
    ...
    
    {Name("Monica")}
    {Icon("Monica_IRL")}
    Santito… ¿no quieres que te ayude a bajar las escaleras?
     
    {Name("Santiago")}
    {Icon("Awelo")}
    Ahhh… tú siempre un encanto.
    Que hermosa chamaca.
    Ven, vamos, bajemos juntos.
    
    {FadeOut(2)} -> cocina
    
== cocina
    {Icon("")}
    {Name("")}
    {Background("Cocina")}
    {FadeIn(2)}
    ...
    
    {Icon("Laura")}
    {Name("Laura")}
    Buenos días familia.
    
    {Icon("")}
    {Name("Todos")}
    ¡Hola, buenos días!
    
    {Icon("Laura")}
    {Name("Laura")}
    Suegro… anoche su hijo no llegó a la casa.
    ¿Le comentó algo de que estuviera trabajando?
    
    {Name("Santiago")}
    {Icon("Awelo")}
    Ay Laurita cariño, si tu que eres su esposa ni te avisa en donde anda tu crees que a su anciano padre siquiera le dirige un hola…
    
    {Icon("Laura")}
    {Name("Laura")}
    Bueno suegro…
    Pues ya a ver si más tarde llega, tal vez sería bueno que hablaran de vez en cuando.
     
    {Name("Santiago")}
    {Icon("Awelo")}
    Lo mismo te digo a ti hija…
    Tu me pelas más que ese desgraciado y eso que yo le he cuidado toda su vida.
    
    {Icon("Laura")}
    {Name("Laura")}
    Suegro… tampoco es tan mal hijo…
    Se esfuerza bastante por nosotros…

    {Icon("")}
    {Name("")}
    ...
    
    {Name("Monica")}
    {Icon("Monica_IRL")}
    Ome, gato goloso, dije pocas y ya fueron muchas.
    
    {Icon("Laura")}
    {Name("Laura")}
    Ay niña… ni que tu se las compraras.
    Yo le voy a dar más salchichas y tu a la escuela, seguro Alex ya se cansó de esperarte.
    
    {Name("Monica")}
    {Icon("Monica/Monica_Asustada")}
    ¡Si es cierto! Pobre Alex
    
    {Name("Monica")}
    {Icon("Monica_IRL")}
    Ya me voy familia.
    
    {Icon("")}
    {Name("Todos")}
    Adios, buen día.
    
    {FadeOut(2)} -> calle
    
== calle
    {Icon("")}
    {Name("")}
    {Background("Calle")}
    {FadeIn(2)}
    ...
    
    {Name("Monica")}
    {Icon("Monica_IRL")}
    Hola Alex ¿Cómo te va en tu mañana?
    
    {Name("Alex")}
    {Icon("Alex_IRL")}
    No me quejo, no hay ni frío ni calor
    Está fina esta mañana
    
    {Name("Monica")}
    {Icon("Monica/Monica_Sonriendo")}
    Tu siempre estás ‘chill’
    Anda, ya vámonos a la escuela.
    
    {Icon("")}
    {Name("")}
    ...
    
    {Name("Monica")}
    {Icon("Monica/Monica_molesta")}
    Alex…
    ¿Está todo bien...?
    ¿no te sientes mal o sí?
    
    {Name("Alex")}
    {Icon("Alex_IRL")}
    No, Moni…
    Lo que pasa es que tengo algo que decirte…
    Pero llegando a la escuela te platico
    
    {Icon("")}
    {Name("")}
    ...
    ...
    ...
    {FadeOut(2)} -> aula
    
== aula
    {Icon("")}
    {Name("")}
    {Background("Aula")}
    {FadeIn(2)}
    ...
    
    {Name("Monica")}
    {Icon("Monica/Monica_molesta")}
    Alex… ¿Ya puedes decirme por favor lo que pasa?
    Me siento hasta mal porque siento que hice algo que te molestó…
    
    {Name("Alex")}
    {Icon("Alex_IRL")}
    No, no es nada de eso...
    Es sólo que...
    Tengo que decirte algo que a mi me duele mucho y no sé cómo decírtelo.
    
    {Name("Monica")}
    {Icon("Monica/Monica_molesta")}
    Cómo es
    El golpe no es más suave porque me lo adornes si es algo que sabes que me va a doler…
    
    {Name("Alex")}
    {Icon("Alex_IRL")}
    Bueno...
    Que conste que tu fuiste la que me dijo que te dijera así de sopetón.
    
    {Name("Monica")}
    {Icon("Monica/Monica_molesta")}
    Hmmm… sí
    Pero dime qué sucede.
    
    {Name("Alex")}
    {Icon("Alex_IRL")}
    ...
    Me voy a mudar Moni
    A mi papá no le está yendo bien en su trabajo y lo van a trasladar a otro estado…

    {Name("Monica")}
    {Icon("Monica/Monica_molesta")}
    ...
    Alex…
    {Icon("Monica/Monica_Sonriendo")}
    Ese tipo de bromas tan pesadas no son divertidas…
    {Icon("Monica/Monica_molesta")}
    Ya deberías saberlo…
    
    {Name("Alex")}
    {Icon("Alex_IRL")}
    No, Moni, es de verdad.
    No sabía cómo decírtelo
    Me siento igual que cuando salí del clóset, yo también estoy muy triste y asustade.
    
    
    {Name("Monica")}
    {Icon("Monica/Monica_molesta")}
    Pero… entonces
    {Icon("Monica/Monica_Asustada")}
    ¿C-Cuándo nos volveremos a ver, te voy a volver a ver siquiera?
    
    {Name("Alex")}
    {Icon("Alex_IRL")}
    Yo…
    Espero que podamos venir a visitar a tu familia 
    Espero poder verte todos los años al menos una vez al año…
    Y a parte podremos mensajearnos todavia
    ... podriamos ... a otros ... juntos
    Tendras mas ... tus cosas ...
    ... ... estara bien ...
    
    {Name("Monica")}
    {Icon("Monica/Monica_Asustada")}
    \*Tranquila*
    \*Respira*
    \*Inhala*
    \*Exhala*
    \*Inhala*
    \*Exhala*
    
    {Icon("Monica/Monica_molesta")}
    Alex, ¿Podemos terminar la conversación en otro momento? 
    No quiero seguir hablando de esto ahora, no me siento bien…
    
    {Name("Alex")}
    {Icon("Alex_IRL")}
    Perdón Moni, no es mi intención lastimarte...
    Sólo comunicarte lo que está pasando en mi vida...
    
    {Name("Monica")}
    {Icon("Monica/Monica_molesta")}
    Ya Alex...
    Déjalo así...
    
    {FadeOut(2)} -> despues_clase
    
== despues_clase
    {Icon("")}
    {Name("")}
    {Background("EntradaEscuela")}
    {FadeIn(2)}
    ...
    
    {Name("Monica")}
    {Icon("Monica/Monica_molesta")}
    \*Tengo que ir a casa* -> opciones_escuela
    
== opciones_escuela
    
    * [Esperar a Alex]
    Tengo que empezar a estar sola… 
    Alex se va a ir de todos modos… -> opciones_escuela
    
    * [Ir a casa]
    Sera mejor asi...
    No me gustan las despedidas
    {FadeOut(2)} -> regreso_casa
    
== regreso_casa
    {Icon("")}
    {Name("")}
    {Background("Cuarto")}
    {FadeIn(2)}
    ...
    
    {Name("Monica")}
    {Icon("Monica/Monica_Pijama")}
    No puedo creer que se vaya a ir...
    No puedo creer que me vaya a quedar sola...
    No se que hacer... -> opciones_dormir
    
== opciones_dormir

    * [Comer algo]
    No puedo ni comer, me duele el estomago... -> opciones_dormir
    
    * [Dormir]
    Creo que deberia descansar
    
    -> END
    
    




