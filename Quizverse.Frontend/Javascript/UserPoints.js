//100 - nTentativas - dica1 - dica2 = pontuação
// Se achado com menos de 3 tentativas = + 150 pontos de bonus
// Se achado com menos de 5 tentativas = + 100 pontos de bonus
// Se achado com menos de 10 tentativas = + 50 pontos de bonus
let allTimeGamePoints = 0;

export function calculateTriesGamePoints(tries, clueUsed1, clueUsed2) {
    let userPoints = 100 - tries;
    if(clueUsed1) {
        userPoints -= 20;
    }

    if(clueUsed2) {
        userPoints -= 20;
    }

    return userPoints;
}

export async function calculateAllTimeGamePoints(userPoints) {

    const userText = document.getElementById('userName').innerText;
    const username = userText.split(':')[1].trim();

    allTimeGamePoints = await getUserPoints(username);

    allTimeGamePoints += userPoints;

    const response = await fetch(`https://localhost:7295/api/UserPoints?userName=${encodeURIComponent(username)}&newUserPoints=${allTimeGamePoints}`, {
        method: 'PUT',
        headers: {
            'Accept': '*/*',
            'Content-Type': 'application/json'
        }
    });

    if (!response.ok) {
        alert("Error!");
    }

    return allTimeGamePoints;
}

export async function getUserPoints(userName) {
    const response = await fetch(`https://localhost:7295/api/UserPoints?userName=${encodeURIComponent(userName)}`, {
        method: 'GET',
        headers: {
            'Accept': '*/*',
            'Content-Type': 'application/json'
        }
    });
  
    if (!response.ok) {
      const errorDetail = await response.text(); 
      throw new Error(`Network response was not ok: ${response.status} - ${errorDetail}`);
    }
  
    const result = await response.json();
    return result.points; 
  }
  
