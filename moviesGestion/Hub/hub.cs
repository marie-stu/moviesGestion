using Microsoft.AspNetCore.SignalR;

namespace Api.Hub;

/// <summary>
/// Ce Hub gère l'envoi de notifications en temps réel aux clients.
/// </summary>
public class NotificationHub : Hub
{
    // Pour cet exemple simple, nous n'avons pas besoin de méthodes que le client appellerait.
    // Le Hub sert uniquement de cible pour que le serveur envoie des messages.
    // On pourrait imaginer une méthode comme `SendMessage` si on voulait faire un chat.
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
