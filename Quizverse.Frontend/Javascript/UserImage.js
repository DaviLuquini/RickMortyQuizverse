export async function getUserImage(username) {
    try {
        const response = await fetch(`https://p01--rickmortyquizverse--2fzvm2y2h546.code.run/api/UserImage?userName=${encodeURIComponent(username)}`, {
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
        return result.image;
        
    } catch (error) {
        console.error(`Erro ao obter imagem do usuário: ${error.message}`);
        return null; 
    }
}


export async function updateUserImage(userImage) {

    const userText = document.getElementById('userName').innerText;
    const username = userText.split(':')[1].trim();

    try {
    const response = await fetch(`https://p01--rickmortyquizverse--2fzvm2y2h546.code.run/api/UserImage?userName=${encodeURIComponent(username)}&newUserImage=${userImage}`, {
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
        console.error(`Erro ao atualizar imagem do usuário: ${error.message}`);
        return 0;
    }
}
  