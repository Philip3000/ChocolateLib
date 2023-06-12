import socket
import requests

IP = '0.0.0.0'
PORT = 14014


s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
#Opretter og lytter på Ip/port
s.bind((IP, PORT))
API_URL = 'https://charlottesstockapi20230611194946.azurewebsites.net/api/EasterEggs/'

while True:
    #Modtager Data og addresse fra klienten
    data, addr = s.recvfrom(1024)
    #Afkoder data.
    data_str = data.decode()
    #Fjerner komma og sætter data værdier i et array
    data_parts = data_str.split(',')

    #tjekker at der er 2 værdier i array'et
    if len(data_parts) == 2:
        #assigner den første værdig
        product_no = data_parts[0].strip()
        #assigner den anden værdi
        amount_sold = data_parts[1].strip()

        #beder om produktet med det specifikke product_no
        response = requests.get(f'{API_URL}{product_no}')
        #Omdanner det til json
        product_info = response.json()
        #Assigner data fra jsonformatet
        inStock = product_info['inStock']
        #Opdaterer lagertallet
        updated_stock = int(inStock) - int(amount_sold)

        #Request body der sendes med i opdateringsmetode
        payload = {
            'productNo': int(product_no),
            'chocolateType': product_info['chocolateType'],
            'price': product_info['price'],
            'inStock': updated_stock
        }
        response = requests.put(f'{API_URL}', json=payload)
        print(updated_stock)
    else:
        print("Invalid data format received.")

