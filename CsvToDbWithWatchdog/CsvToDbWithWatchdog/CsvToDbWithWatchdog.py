import os
import time
import pandas as pd
import requests
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

# take data from csv document, transform it, call API to get data to DB
def csv_to_db (file_path):
    # TEST THAT CAN BE TAKEN AWAY IN THE END
    print(f"\nthe file: {file_path}")

    # wanted columns from csvFile
    NeedColumns = ['Frisør' , 'Adresse' , 'Adresse 2' , 'Hjemmeside']

    # data frame , df
    df = pd.read_csv(file_path , delimiter=';', usecols=NeedColumns)

    # delete all datapoints with no webpage/hjemmeside
    df = df[df['Hjemmeside'].notnull() & (df['Hjemmeside'] != '')]

    # combine address sting 
    df['fullAddress'] = df['Adresse'] + ', ' + df['Adresse 2']

    # Build DTO to API call from rows in df
    def build_dto(row):
        return {
            "salonName": row['Frisør'],
            "website": row['Hjemmeside'],
            "address": row['fullAddress']
        }

    dtoList = [build_dto(row) for _, row in df.iterrows()]

    # test to view the transformed data format - CAN BE DELETED IN THE END
    for dto in dtoList: print (dto)

    # POST request to API 
    api_url = "https://localhost:7001/api/Hairdressers"
    headers = {'Content-Type': 'application/json'}

    for dto in dtoList:
        response = requests.post(api_url, json=dto, headers=headers, verify=False)  # verify=False kun til lokal dev!
        print(response.status_code, response.text)

    # TEST THAT CAN BE TAKEN AWAY IN THE END
    print(f"\nTransfor done: {len(dtoList)} files transported.")

# help funktion: check if the file is finished writing = file size 
def wait_until_file_is_ready (file_path, timeout=5):
    last_size = -1 # negative to always trigger 1 run of the if statement. 

    # check file size changes for 5 seconds
    for _ in range(timeout * 2): # iterates 10 times - end with being 5 sek.
        if not os.path.exists(file_path): # chech if the fil exist - watchdog trigges, but sometimes the file dosn't exist true yet 
            time.sleep(0.5)
            continue

        current_size = os.path.getsize(file_path)

        if current_size == last_size:
            return True # the file is ready

        last_size = current_size

        time.sleep(0.5)
    
    # the file did not get stable within the 5 seconds
    print(f"The file '{file_path}' did not get stabil before timeout")
    return False

# watchdog observation
