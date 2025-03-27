package com.example.assignment

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.google.firebase.database.ktx.database
import com.google.firebase.ktx.Firebase

class RegisterActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register)

        val buttonRegister = findViewById<Button>(R.id.buttonLogin)
        val buttonBack = findViewById<Button>(R.id.buttonMainRegister)

        buttonBack.setOnClickListener {
            val intent = Intent(this, MainActivity::class.java)
            startActivity(intent)
        }

        buttonRegister.setOnClickListener {
            register()
        }
    }

    private fun register() {
        // variables
        val editTextUsername = findViewById<EditText>(R.id.editTextUser)
        val editTextPassword = findViewById<EditText>(R.id.editTextPassword)

        // if there is no input, ask them to enter
        if (editTextUsername.text.isEmpty()) {
            editTextUsername.error = "Username required!"
            return
        }

        if (editTextPassword.text.isEmpty()) {
            editTextPassword.error = "Password required!"
            return
        }

        // import data to the database and do validation
        val database = Firebase.database("https://assignment-recipe-default-rtdb.asia-southeast1.firebasedatabase.app/")
        val myRef = database.getReference("Account/" + editTextUsername.text.toString())

        myRef.get().addOnSuccessListener {
            // if no one register before
            if (it.value == null) {
                myRef.setValue(editTextPassword.text.toString())
                Toast.makeText(this@RegisterActivity, "Registration Success!", Toast.LENGTH_SHORT).show()

            } else {
                // if registered before
                if (it.value != null ) {
                    Toast.makeText(this@RegisterActivity, "User already exist, please try with another one.", Toast.LENGTH_SHORT).show()

                }
            }
        }.addOnFailureListener {
            Toast.makeText(this, "Connection fail, Try again.", Toast.LENGTH_SHORT).show()

        }

    }
}