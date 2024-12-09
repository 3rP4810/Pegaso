#Per eseguire il codice è necessario installare la libreria lxml
import pandas as pd
import os

path = input('Path del file xml:\n')
name = os.path.splitext(os.path.basename(path))[0]

df = pd.read_xml(path)
df.to_csv(f'{name}.csv', sep=';', index=False)