//100 - nTentativas - dica1 - dica2 = pontuação
// Se achado com menos de 3 tentativas = + 150 pontos de bonus
// Se achado com menos de 5 tentativas = + 100 pontos de bonus
// Se achado com menos de 10 tentativas = + 50 pontos de bonus

//Pontos no laderboard apenas após 10 jogos


export function calculateGamePoints(tries, clueUsed1, clueUsed2) {
    let userPoints = 100 - tries;
    if(clueUsed1) {
        userPoints -= 20;
    }

    if(clueUsed2) {
        userPoints -= 20;
    }

    return userPoints;
}


export function calculateAllTimeGamePoints(userPoints) {
    let allTimeGamePoints =+ userPoints;

    return allTimeGamePoints;
}

