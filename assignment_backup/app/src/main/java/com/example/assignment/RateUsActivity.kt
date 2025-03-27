package com.example.assignment


import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.RatingBar
import android.widget.Toast
import com.google.firebase.database.DataSnapshot
import com.google.firebase.database.DatabaseError
import com.google.firebase.database.ValueEventListener
import com.google.firebase.database.ktx.database
import com.google.firebase.database.ktx.getValue
import com.google.firebase.ktx.Firebase


class RateUsActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_rate_us)

        val buttonConfirm = findViewById<Button>(R.id.ConfirmButton)
        buttonConfirm.setOnClickListener {
            confirm()
        }
    }

    private fun confirm() {
        // variables
        val ratingBar = findViewById<RatingBar>(R.id.ratingBar).rating.toString()
        var editTextFeedback = findViewById<EditText>(R.id.EditTextFeedback).text.toString()
        val username = intent.getStringExtra("username")

        //if feedback is empty, assign "no feedback" to var feedback
        if (editTextFeedback.isEmpty()) {
            editTextFeedback = "No Feedback"
        }

        //import data to database
        val database = Firebase.database("https://assignment-recipe-default-rtdb.asia-southeast1.firebasedatabase.app/")
        val myRef = database.getReference("Rating/$username")
        myRef.setValue("$ratingBar , $editTextFeedback")

        //retrieve data from database
        myRef.addValueEventListener(object: ValueEventListener {

            override fun onDataChange(snapshot: DataSnapshot) {
                val readRating = snapshot.getValue<String>()
                Toast.makeText(this@RateUsActivity, "Thank You!\nRating: $readRating", Toast.LENGTH_SHORT).show()
            }

            override fun onCancelled(error: DatabaseError) {
                Toast.makeText(this@RateUsActivity, "Connection fail, Try again", Toast.LENGTH_SHORT).show()
            }

        })

    }
}

