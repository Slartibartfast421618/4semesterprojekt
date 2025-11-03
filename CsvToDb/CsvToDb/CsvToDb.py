import pandas as pd

# read csv fil - give directions to fil location
    # MUST BE SET FOR YOU PERSONALY
csvFile = 'C:\\Users\\trine\\source\\repos\\4semesterprojekt\\CsvToDb\\HairdresserCsv\\5FrisørMedHjemmesider.csv'

# wanted columns from csvFile
NeedColumns = ['Frisør' , 'Adresse' , 'Adresse 2' , 'Hjemmeside']

# data frame , df
df = pd.read_csv(csvFile , delimiter=';', usecols=NeedColumns)

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

# test to view the transformed data format
for dto in dtoList: print (dto)

# POST to API



