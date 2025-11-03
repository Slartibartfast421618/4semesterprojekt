import pandas as pd

# read csv fil - give directions to fil location
    # MUST BE SET FOR YOU PERSONALY
df = pd.read_csv('C:\\Users\\trine\\source\\repos\\4semesterprojekt\\CsvToDb\\HairdresserCsv\\5FrisørMedHjemmesider.csv')

# Sort wanted columns
NeedColumns = ['Frisør' , 'Adresse' , 'Adresse 2' , 'Hjemmeside']
df = df[NeedColumns]

# combine address sting 




