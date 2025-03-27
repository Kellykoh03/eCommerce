package com.example.assignment

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.google.firebase.database.ktx.database
import com.google.firebase.ktx.Firebase


class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val buttonRegister = findViewById<Button>(R.id.buttonMainRegister)
        val buttonLogin = findViewById<Button>(R.id.buttonLogin)

        buttonRegister.setOnClickListener {
            val intent = Intent(this, RegisterActivity::class.java)
            startActivity(intent)
        }

        buttonLogin.setOnClickListener {
            login()
        }
    }

    private fun login() {
        // variables
        val editTextUsername = findViewById<EditText>(R.id.editTextUser)
        val editTextPassword = findViewById<EditText>(R.id.editTextPassword)

        // if there is no input, ask them to enter
        if (editTextUsername.text.isEmpty()) {
            editTextUsername.error = "Username Required!"
            return
        }

        if (editTextPassword.text.isEmpty()) {
            editTextPassword.error = "Password Required!"
            return
        }

        // read data to the database
        val database = Firebase.database("https://assignment-recipe-default-rtdb.asia-southeast1.firebasedatabase.app/")
        val myRef = database.getReference("Account/" + editTextUsername.text.toString())

        myRef.get().addOnSuccessListener {
            // if user not exist
            if (it.value == null) {
                Toast.makeText(this@MainActivity, "User Not found!", Toast.LENGTH_SHORT).show()

            } else {
                // if password correct
                if (it.value == editTextPassword.text.toString()) {
                    val intent = Intent(this, HomeActivity::class.java)
                    intent.putExtra("username", "${editTextUsername.text}")
                    startActivity(intent)

                } else {
                    // if password wrong
                    if (it.value != editTextPassword.text.toString()) {
                        Toast.makeText(this@MainActivity, "Incorrect Password", Toast.LENGTH_SHORT).show()

                    }
                }
            }
        }.addOnFailureListener {
            // if unable to connect to database
            Toast.makeText(this, "Connection fail, Try again.", Toast.LENGTH_SHORT).show()

        }
    }
}