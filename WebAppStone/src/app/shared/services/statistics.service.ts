import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
  private hubConnection: signalR.HubConnection | undefined;

  public startConnection = (callBack: Function) => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.api_base_url}/statisticshub`)
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));

    this.hubConnection.on('Notify', (message) => {
      callBack();
    });
  };
}
