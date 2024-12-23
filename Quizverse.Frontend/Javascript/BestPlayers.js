export async function getBestPlayers() {
    try {
        const response = await fetch(`https://p01--rickmortyquizverse--2fzvm2y2h546.code.run/api/UserPoints/all`, {
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

        const users = result.userPointsList.map(user => ({
            name: user.name,
            userpoints: user.userPoints
        }));

        const sortedUsers = users.sort((a, b) => b.userpoints - a.userpoints);

        return sortedUsers;
        
    } catch (error) {
        console.error(`Erro ao obter melhores usuários: ${error.message}`);
        return null; 
    }
}