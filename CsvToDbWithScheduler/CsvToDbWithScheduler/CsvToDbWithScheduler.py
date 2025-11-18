from os import path
from sched import scheduler
import pandas as pd
import requests
from pathlib import Path
from apscheduler.schedulers.background import BackgroundScheduler
from datetime import datetime
import time
import json

# Folder to watch
base_dir = Path(__file__).resolve().parents[1]
folder_to_watch = base_dir / "HairdresserCsv"

# allready processed files - .JSON 
state_file = base_dir / "processed_files.json"
if state_file.exists():
    with open(state_file, "r") as f:
        processed_files = set(json.load(f))
    print(f"Found existing file. {state_file.name}")
else:
    processed_files = set()
    print("No existing file found. New file processed_file.json created.")

# help function - save new states for processed files
def save_state():
    with open(state_file, "w") as f:
        json.dump(list(processed_files), f)
        print(f"Saved new processed file in {state_file.name}")

# take data from csv document, transform it, call API to get data to DB
def csv_to_db (csv_file: Path):
    # TEST THAT CAN BE TAKEN AWAY IN THE END
    print(f"\nthe file: {csv_file.name}")

    # wanted columns from csvFile
    NeedColumns = ['Frisør' , 'Adresse' , 'Adresse 2' , 'Hjemmeside']

    # data frame , df
    df = pd.read_csv(csv_file , delimiter=';', usecols=NeedColumns)

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

# check if there are new csv files in the folder
def check_folder():
    print(f"[{datetime.now():%d-%m-%Y %H:%M:%S} Check for new CVS files..]")
    for csv_file in folder_to_watch.glob("*.csv"):
        if csv_file.name not in processed_files:
            print(f"new file found: {csv_file.name}") 
            csv_to_db(csv_file) # call function
            processed_files.add(csv_file.name)
            save_state()
    print("Check done.\n")

#APScheduler setup
scheduler = BackgroundScheduler()
scheduler.add_job(check_folder, 'interval', minutes = 1) # test scenario
# scheduler.add_job(check_folder, 'cron', day='1-7', day_of_week='sun', hour=2) # FUTURE production scenario
scheduler.start()

# initial check 
check_folder()

print(f"Watching {folder_to_watch}. ")
print("Press Ctrl+c to close the script.\n")

# to keep the script running and react on close command
try:
    while True:
        time.sleep(60)
except KeyboardInterrupt:
    scheduler.shutdown()
    print("The script has been stopped.")
