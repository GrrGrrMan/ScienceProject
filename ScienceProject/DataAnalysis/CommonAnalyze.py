import os
import re
from collections import Counter

# Constants
JUST_CHECK_TXT = 1
CHECK_FOLDER = 2
CHECK_SUBFOLDER = 3

# Set the mode
mode = CHECK_SUBFOLDER

# Set the folder path
folder_path = r'C:\Users\jiuma\Downloads\Z Rando Temp\DataAnylize\Files\results'

# Set the range to check
analyze_range = 11

def get_numbers_and_letters(file_path):
    with open(file_path, 'r', encoding='utf-8', errors='ignore') as file:
        lines = file.readlines()
        content = ''
        for line in lines[1:]:  # Skip the first line
            content += line

    numbers = []
    for char in content:
        if char.isdigit():
            numbers.append(int(char))

    letters = []
    for char in content:
        if char.isalpha():
            letters.append(char)

    return numbers, letters

def get_most_common(stuff, n):
    counter = Counter(stuff)
    most_common = []
    for item, count in counter.items():
        most_common.append((item, count))
    most_common.sort(key=lambda x: x[1], reverse=True)
    return most_common[:n]

def check_folder(folder_path, analyze_range):
    all_numbers = []
    all_letters = []

    for file_name in os.listdir(folder_path):
        if file_name.endswith('.txt'):
            file_path = os.path.join(folder_path, file_name)
            numbers, letters = get_numbers_and_letters(file_path)
            all_numbers += numbers
            all_letters += letters

    most_common_numbers = get_most_common(all_numbers, analyze_range)
    most_common_letters = get_most_common(all_letters, analyze_range)

    print(f'Checking folder: {folder_path}')
    print('Most common numbers:')
    for num, count in most_common_numbers:
        print(f'{num}: {count}')

    print('Most common letters:')
    for letter, count in most_common_letters:
        print(f'{letter}: {count}')
    print()

def check_subfolders(folder_path, analyze_range):
    subfolders = []
    for name in os.listdir(folder_path):
        if os.path.isdir(os.path.join(folder_path, name)):
            subfolders.append(os.path.join(folder_path, name))
    subfolders.sort(key=lambda x: int(os.path.basename(x)))

    all_numbers = []
    all_letters = []

    for subfolder in subfolders:
        for file_name in os.listdir(subfolder):
            if file_name.endswith('.txt'):
                file_path = os.path.join(subfolder, file_name)
                numbers, letters = get_numbers_and_letters(file_path)
                all_numbers += numbers
                all_letters += letters

        most_common_numbers = get_most_common(all_numbers, analyze_range)
        most_common_letters = get_most_common(all_letters, analyze_range)

        print(f'Checking folder up to: {subfolder}')
        print('Most common numbers:')
        for num, count in most_common_numbers:
            print(f'{num}: {count}')

        print('Most common letters:')
        for letter, count in most_common_letters:
            print(f'{letter}: {count}')
        print()

def main():
    global folder_path
    if not os.path.isdir(folder_path):
        print(f"Error: The folder path '{folder_path}' does not exist.")
        return

    global mode
    if mode == JUST_CHECK_TXT:
        check_folder(folder_path, analyze_range)
    elif mode == CHECK_FOLDER:
        check_folder(folder_path, analyze_range)
    elif mode == CHECK_SUBFOLDER:
        check_subfolders(folder_path, analyze_range)
    else:
        print("Error: Invalid mode selected.")

main()
