//100 - nTentativas - dica1 - dica2 = pontuação
// Se achado com menos de 3 tentativas = + 150 pontos de bonus
// Se achado com menos de 5 tentativas = + 100 pontos de bonus
// Se achado com menos de 10 tentativas = + 50 pontos de bonus
let allTimeGamePoints = 0;

export function calculateTriesGamePoints(tries, clueUsed1, clueUsed2) {
    let userPoints = 100 - tries * 3;
    if(clueUsed1) {
        userPoints -= 20;
    }

    if(clueUsed2) {
        userPoints -= 60;
    }

    if(userPoints <= 0) {
        return 0;
    }

    return userPoints;
}

export async function calculateAllTimeGamePoints(userPoints) {

    const userText = document.getElementById('userName').innerText;
    const username = userText.split(':')[1].trim();

    allTimeGamePoints = await getUserPoints(username);

    allTimeGamePoints += userPoints;

    try {
    const response = await fetch(`https://localhost:7295/api/UserPoints?userName=${encodeURIComponent(username)}&newUserPoints=${allTimeGamePoints}`, {
        method: 'PUT',
        headers: {
            'Accept': '*/*',
            'Content-Type': 'application/json'
        }
    });

    if (!response.ok) {
        throw new Error(`Network response was: ${response.status}`);
    }

    }catch(error) {
        console.error(`Erro ao atualizar pontos do usuário: ${error.message}`);
        return 0;
}

    return allTimeGamePoints;
}
  
  export async function getUserPoints(userName) {
    try {
        const response = await fetch(`https://localhost:7295/api/UserPoints?userName=${encodeURIComponent(userName)}`, {
            method: 'GET',
            headers: {
                'Accept': '*/*',
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`Network response was not ok: ${response.status}`);
        }

        const result = await response.json();
        return result.points;
        
    } catch (error) {
        console.error(`Erro ao obter pontos do usuário: ${error.message}`);
        return 0; 
    }
}
