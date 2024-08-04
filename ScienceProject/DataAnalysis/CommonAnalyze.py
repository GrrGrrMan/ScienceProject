from collections import Counter
import re

analyze_range = 11  #Check though the range, like #1 most common, #2 most common.
                    #this constant makes it #1-"anylyze_range" most common

def extract_numbers_and_characters(file_path):
    with open(file_path, 'r') as file:
        content = file.read().replace('\n', '')  # Remove line breaks


    numbers = re.findall(r'\d', content)
    numbers = list(map(int, numbers))

    characters = re.findall(r'[a-zA-Z]', content) #No commas needed in the table range

    return numbers, characters

def analyze_frequencies(elements, n):
    if not elements:
        return []
    counter = Counter(elements)
    most_common_elements = counter.most_common(n)
    return most_common_elements
file_path = r'C:\PipthonRun\DataAnylize\Files\1.txt'

numbers, characters = extract_numbers_and_characters(file_path)

most_common_numbers = analyze_frequencies(numbers, analyze_range)
print('Most common numbers:')
for element, count in most_common_numbers:
    print(f'{element}: {count}')

most_common_characters = analyze_frequencies(characters, analyze_range)
print('Most common characters:')
for element, count in most_common_characters:
    print(f'{element}: {count}')
