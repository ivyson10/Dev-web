let cvs, ctx, pers, inimigo;

class Personagem {
    constructor(nome, nvel, nR, nG, nB, nr = 1, nx = 0, ny = 0) {
        this.nome = nome;
        this.x = nx;
        this.y = ny;
        this.raio = nr;
        this.corR = nR;
        this.corG = nG;
        this.corB = nB;
        this.vel = nvel;
    }
    cima(){
        if((this.y-(this.raio+4)) >= 0 && (this.y-(this.raio+4)<=1350)){
        this.y -= this.vel;
    }
    }
    baixo(){
        if((this.y+(this.raio+4)) <= 600 && (this.y+(this.raio+4)<=1350)){
        this.y += this.vel;
        }
        
    }
    esquerda(){
        if((this.x-(this.raio+4)) >= 0 && (this.x-(this.raio+4)<=1350)){
        this.x -= this.vel;
    }
    }
    direita(){
        if((this.x+(this.raio+4)) >= 0 && (this.x+(this.raio+4)<=1350)){
        this.x += this.vel;
        }
    }
    seDesenhe(){
        ctx.beginPath();
        ctx.arc(this.x, this.y,
                this.raio, 0, 2*Math.PI);
        ctx.fillStyle = `rgb(${this.corR},${this.corG},${this.corB})`;
        ctx.fill();
        ctx.fillStyle = 'black';
        ctx.font = '20px Arial';
        ctx.fillText(this.nome, this.x, this.y);
    }
    comeu(comida){
        this.raio += 5;

        this.corR = (this.corR + comida.corR)/2;
        this.corG = (this.corG + comida.corG)/2;
        this.corB = (this.corB + comida.corB)/2;
    }
    
}

window.onload = function() {
    cvs = document.getElementById('tela');
    ctx = cvs.getContext('2d');

    pers = new Personagem('Ameba', 10, 255, 0, 0, 50, 400, 300);
    inimigo = new Personagem('Nada', 10, 0, 255, 0, 10, 700, 550);

    window.addEventListener('keydown', movePersonagem);

    requestAnimationFrame(atualizaTela);
}

function movePersonagem(event){
    switch(event.keyCode){
        case 37:// esquerda
            pers.esquerda();
            break;
        case 38:// cima
            pers.cima();
            break;
        case 39:// direita
            pers.direita();
            break;
        case 40:// baixo
            pers.baixo();
            break;
    }
    if( Math.pow(pers.raio + inimigo.raio, 2) >= 
        Math.pow(pers.x - inimigo.x, 2)
        + Math.pow(pers.y - inimigo.y, 2) ){
            pers.comeu(inimigo);
            
            inimigo.x = Math.random()*1350;
            inimigo.y = Math.random()*600;
            inimigo.corR = Math.random()*255;
            inimigo.corG = Math.random()*255;
            inimigo.corB = Math.random()*255;
        }
}

function atualizaTela(){
    ctx.clearRect(0,0, 1350,600);

    pers.seDesenhe();
    inimigo.seDesenhe();

    requestAnimationFrame(atualizaTela);
}