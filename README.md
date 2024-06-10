# Voting System with Biometric (Fingerprint) Authentication
 

## Overview

This project implements a voting system using fingerprint verification. It allows voters to authenticate themselves using their fingerprints and cast their votes. The system consists of multiple forms for various functionalities like connecting to the fingerprint sensor, verifying voters, casting votes, and viewing results.

## Project Structure

- **form1.cs**: The main form that handles the initial setup and connection to the fingerprint sensor.
- **form2.cs**: The form for voter and organizer authentication via fingerprint.
- **form3.cs**: The form for casting votes.
- **form4.cs**: The form for displaying voting results and announcing the winner.
- **class1.cs**: Contains global variables and utility functions.
- **main.cs**: Contains the main entry point of the application and defines the classes for voters and candidates.

## Forms and Their Functionalities

### Form1

- **Purpose**: Initial setup and connection to the fingerprint sensor.
- **Key Elements**:
  - `button1`: Opens the form for voter or organizer authentication (Form2).
  - `button2`: Connects to the fingerprint sensor via a specified COM port.
  - `button3`: Loads fingerprint data from a file.
- **Events**:
  - `button1_Click`: Opens Form2.
  - `Form1_FormClosing`: Saves candidate votes to a file before closing.
  - `button2_Click`: Connects to the fingerprint sensor.
  - `button3_Click`: Loads fingerprint data from a file.

### Form2

- **Purpose**: Handles authentication of voters and the organizer via fingerprint verification.
- **Key Elements**:
  - `textBox1`: Input field for voter ID.
  - `button1`: Triggers fingerprint verification.
- **Functions**:
  - `testFingerPrint`: Verifies the fingerprint.
  - `getImageButton`: Acquires and saves fingerprint images.
- **Events**:
  - `button1_Click`: Verifies the voter or organizer based on the entered ID.

### Form3

- **Purpose**: Allows authenticated voters to cast their votes.
- **Key Elements**:
  - `flowLayoutPanel1`: Displays the list of candidates as radio buttons.
  - `button1`: Submits the vote.
- **Events**:
  - `button1_Click`: Submits the selected vote and updates the vote count.

### Form4

- **Purpose**: Displays voting results and announces the winner.
- **Key Elements**:
  - `button1`: Displays the vote count for each candidate.
  - `button2`: Announces the winner or detects a tie.
- **Events**:
  - `button1_Click`: Displays the vote counts.
  - `button2_Click`: Determines and announces the winner.

## Classes

### Global (class1.cs)

- Contains global variables and utility functions.
- **Functions**:
  - `ConvertToHex`: Converts a hex string to a byte array.

### Human (main.cs)

- **Abstract Class**: Base class for `Voter` and `Candidate`.
- **Properties**:
  - `Name`: The name of the person.
  - `NationalID`: The national ID of the person.
- **Abstract Method**:
  - `CreateList`: Initializes the list of persons from a file.

### Voter (main.cs)

- Inherits from `Human`.
- **Properties**:
  - `hasVoted`: Indicates if the voter has voted.
- **Static List**:
  - `v`: List of voters.
- **Method**:
  - `CreateList`: Initializes the list of voters from a file.

### Candidate (main.cs)

- Inherits from `Human`.
- **Properties**:
  - `CountVotes`: The number of votes received.
- **Static List**:
  - `c`: List of candidates.
- **Method**:
  - `CreateList`: Initializes the list of candidates from a file.

### Test (main.cs)

- **Serializable Class**: Used to store fingerprint data.
- **Properties**:
  - `charBuffer`: Byte array representing the fingerprint data.

## How to Run the Project

1. **Set Up Environment**:
   - Ensure you have the necessary hardware, including a fingerprint sensor.
   - Install the required software dependencies (e.g., .NET Framework).

2. **Connect the Fingerprint Sensor**:
   - Run the application and enter the correct COM port number.
   - Click the "Connect" button to establish a connection with the fingerprint sensor.

3. **Load Fingerprint Data**:
   - Load existing fingerprint data from a file if available.

4. **Authenticate and Vote**:
   - Enter the voter ID and authenticate using the fingerprint sensor.
   - Cast your vote if authentication is successful.

5. **View Results**:
   - The organizer can view the voting results and announce the winner.

## Files

- `Voters.txt`: Stores voter information.
- `Candidates.txt`: Stores candidate information and their vote counts.

## Notes

- Ensure the fingerprint sensor is properly connected and configured.
- Handle exceptions and errors appropriately to ensure smooth operation.
- Maintain the integrity and security of the voter and candidate data.
