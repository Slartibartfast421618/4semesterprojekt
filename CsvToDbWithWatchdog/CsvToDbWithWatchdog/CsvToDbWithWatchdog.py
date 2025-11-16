import os
import time
import pandas as pd
import requests
from pathlib import Path
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

# help function: check if the file is finished writing = file size 
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

# watchdog event handler 
class CsvHandler(FileSystemEventHandler):
    def on_created(self, event):
        # only react on new csv files in the folder
        if not event.is_directory and event.src_path.lower().endswith(".csv"):
            print(f"\n New CSV: {event.src_path}")

            # check if file is ready
            if wait_until_file_is_ready(event.src_path):
                csv_to_db(event.src_path)
            else:
                print (f"File {event.src_path} not able to be read, because of timeout from check if file is ready.")

# watchdog observation
if __name__ == "__main__":
    # the folder to watch
    base_dir = Path(__file__).resolve().parents[1]
    folder_to_watch = base_dir / "HairdresserCsv"

    # if the folder to watch do not exist
    if not folder_to_watch.exists():
        raise FileNotFoundError(f"Folder not found: {folder_to_watch}")

    event_handler = CsvHandler()
    observer = Observer()
    observer.schedule(event_handler, str(folder_to_watch), recursive=False) # (where to watch, what to do, only this folder/no sub folders )
    observer.start()

    print(f"Watching {folder_to_watch}. ")
    print("Press Ctrl+c to close the script.\n")
    
    try: 
        while True: # infinity loop, with 60 seconds delay 
            time.sleep(60)
    except KeyboardInterrupt: # whene Ctrl+C pressed
        observer.stop()
    observer.join() # full clean up of threads before closing the script. 
