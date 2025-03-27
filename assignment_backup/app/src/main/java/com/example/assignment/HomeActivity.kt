package com.example.assignment

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AlertDialog


class HomeActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home)

        val buttonRecipe = findViewById<Button>(R.id.RecipeButton)
        val buttonRateUs = findViewById<Button>(R.id.RateUsButton)
        val buttonLogOut = findViewById<Button>(R.id.buttonLogOut)
        val username = intent.getStringExtra("username")

        Toast.makeText(this, "Welcome back $username,\nlet's start CooKing.", Toast.LENGTH_LONG).show()

        buttonRecipe.setOnClickListener {
            val intent = Intent(this, RecipeActivity::class.java)
            startActivity(intent)
        }

        buttonRateUs.setOnClickListener {
            val intent = Intent(this, RateUsActivity::class.java)
            intent.putExtra("username", "${username.toString()}")
            startActivity(intent)
        }

        buttonLogOut.setOnClickListener {
            val intent = Intent(this, MainActivity::class.java)
            startActivity(intent)
        }
    }
}